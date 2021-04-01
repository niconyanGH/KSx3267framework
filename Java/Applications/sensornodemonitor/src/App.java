import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import com.fazecast.jSerialComm.SerialPort;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.chart.LineChart;
import javafx.scene.control. *;
import javafx.scene.layout.FlowPane;
import javafx.stage.Stage;

public class App extends Application {

    private FlowPane sensorflowpane;
    private ReadSensorThread readThread;
    private KSXNode mIntergatedNode;
    STDModbusMaster mMaster = new STDModbusMaster();
    private List<PaneSensor> mPanesensorlist = new ArrayList<>();
    private Label lbinfo;
    private Label lbscaninfo;
    public Label lbItv;
    public LineChart mchart;

    public ComboBox cbportList;
    public Button btnOpen;
    public Button btnScan;
    public Button btnDvc;

    @Override public void start(Stage stage)throws IOException {

        Parent root = FXMLLoader.load(getClass().getResource("mainframe.fxml"));
        ScrollPane sclsensorflowpane = (ScrollPane)root.lookup("#sclsensor");
        sensorflowpane = (FlowPane)sclsensorflowpane.getContent();

        lbinfo = (Label)root.lookup("#label_nodeinfo");
        lbscaninfo = (Label)root.lookup("#label_scan");
        lbItv = (Label)root.lookup("#labelid1");
        mchart = (LineChart)root.lookup("#sensorchart");
        mchart.setAnimated(false);

        cbportList = (ComboBox)root.lookup("#comboportList");
        btnOpen = (Button)root.lookup("#btnopen");
        btnScan = (Button)root.lookup("#button_scan");
        btnDvc = (Button)root.lookup("#button_device");

        ComboBox cbitv = (ComboBox)root.lookup("#combointerval");

        SerialPort[] ports = SerialPort.getCommPorts();

        btnScan.setDisable(true);
        btnDvc.setDisable(true);
        lbItv.setDisable(true);
        cbitv.setDisable(true);
        for (SerialPort pp : ports) {
            cbportList.getItems().add(pp.getDescriptivePortName());
        }

        btnOpen.setOnAction((event) -> {
            System.out.println(cbportList.getSelectionModel().getSelectedItem());
            for (SerialPort pp : ports) {
                if (pp.getDescriptivePortName() == cbportList.getSelectionModel().getSelectedItem()) 
                    OpenPort(pp.getSystemPortName());
                }
            });

        btnScan.setOnAction((event) -> {
            NodeScan();
            btnOpen.setDisable(true);
            btnScan.setDisable(false);
            btnDvc.setDisable(false);
        });

        btnDvc.setOnAction((event) -> {
            DeviceScan();
            btnScan.setDisable(true);
            btnDvc.setDisable(true);
            lbItv.setDisable(false);
            cbitv.setDisable(false);
        });

        cbitv.getItems().add("읽지않음");
        for (int i = 0; i <= 30; i++) {
            cbitv.getItems().add("" + i);
        }
        cbitv.getSelectionModel().select(0);
        cbitv.setOnAction((e) -> {
            String selstr = (String)cbitv.getSelectionModel().getSelectedItem();
            if (selstr.contains("0") == true) {
                readThread.Stopthread();
                readThread = null;
            } else if (selstr.contains("읽지않음") == true) {
                readThread.Clearthread();
                readThread.Stopthread();
                readThread = null;
                btnDvc.setDisable(false);
                lbItv.setDisable(true);
                cbitv.setDisable(true);
            } else {
                int inum = Integer.parseInt(selstr);
                if (readThread == null) {
                    readThread = new ReadSensorThread(mIntergatedNode, mPanesensorlist);
                    readThread.StartThread();
                }
                readThread.timeintervalmsec = inum * 1000;
            }

        });

        stage.setScene(new Scene(root));
        stage.show();
    }

    public void NodeScan() {
        PRODUCT_TYPE mNodetype = PRODUCT_TYPE.NONE;
        int slaveid = 0;
        String strinfo;
        for (int i = 0; i < 20; i++) {
            strinfo = "국번= " + i;
            mNodetype = KSX326xCommon.IsKSXNode(i, mMaster);
            if (mNodetype != PRODUCT_TYPE.NONE) {
                slaveid = i;
                strinfo += " , 장비연결됨...";
                i = 10000;
            } else {
                strinfo += " , 장비없음...";
            }
            lbscaninfo.setText(strinfo);
        }

        if (mNodetype == PRODUCT_TYPE.SENSORNODE) {
            mIntergatedNode = new SensorNode(slaveid, mMaster);
            String cs = " 기본정보 읽기 실패.";
            if (mIntergatedNode.ReadNodeInformation() == true) {
                cs = " CertificateAuthority = " + mIntergatedNode.mNodeInfo.CertificateAuthority +
                        "\r\n";
                cs += " CompanyCode = " + mIntergatedNode.mNodeInfo.CompanyCode + "\r\n";
                cs += " ProductType = " + mIntergatedNode.mNodeInfo.ProductType + "\r\n";
                cs += " ProductCode = " + mIntergatedNode.mNodeInfo.ProductCode + "\r\n";
                cs += " ChannelNumber = " + mIntergatedNode.mNodeInfo.ChannelNumber + "\r\n";
                cs += " ProtocolVersion = " + mIntergatedNode.mNodeInfo.ProtocolVersion + "\r\n";
            }

            lbinfo.setText(cs);
        }
    }

    public void DeviceScan() {
        try {
            if (mIntergatedNode.ReadDeviceCodeList() == true) {
                if (mIntergatedNode.CreateDevices() == true) {
                    System.out.println(" 디바이스갯수 = " + mIntergatedNode.mDevices.size());

                    sensorflowpane.getChildren().clear();

                    for (SensorDev msdev : mIntergatedNode.mSensorDevices) {
                        PaneSensor ps = PaneSensor.Create(this, msdev);
                        sensorflowpane.getChildren().add(ps);
                        mPanesensorlist.add(ps);
                        ps.setOnMouseClicked(event -> OnSensorPaneClick(ps));
                    }
                }
            }
        } catch (IOException e) {}
    }

    public void OpenPort(String tmpPort) {
        if (mMaster.Open(tmpPort, 9600) == false) {
            System.out.println("통신포트를 열수 없읍니다. ");
        } else {
            cbportList.setDisable(true);
            btnOpen.setDisable(true);
            btnScan.setDisable(false);
        }
    }

    public void OnSensorPaneClick(PaneSensor ps) {
        mchart.getData().clear();
        mchart.getData().add(ps.mSeries);
    }

    public static void main(String[] args) {
        launch(args);
    }

}