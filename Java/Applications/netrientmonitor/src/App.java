import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import com.fazecast.jSerialComm.SerialPort;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.FlowPane;
import javafx.stage.Stage;



public class App extends Application {

    private FlowPane sensorflowpane;
    private ReadSensorThread readThread;
    private IntergratedNode mIntergatedNode;
    STDModbusMaster mMaster = new STDModbusMaster();
    private List<PaneSensor> mPanesensorlist = new ArrayList<>();
    private Label lbinfo;
    private Label lbscaninfo;

    private int controllevel = 0;
    private ComboBox<String> comboBox_start;
    private ComboBox<String> comboBox_end;
    private TextField textBox_on_sec;
    private TextField textBox_ec;
    private TextField textBox_ph;

   
    @Override
    public void start(Stage stage) throws IOException {

        Parent root = FXMLLoader.load(getClass().getResource("mainframe.fxml"));
        ScrollPane sclsensorflowpane = (ScrollPane) root.lookup("#sclsensor");
        sensorflowpane = (FlowPane) sclsensorflowpane.getContent();

        lbinfo = (Label) root.lookup("#label_nodeinfo");
        lbscaninfo = (Label) root.lookup("#label_scan");

        ComboBox<String> cbcom = (ComboBox) root.lookup("#cb_comlist");

        SerialPort[] ports = SerialPort.getCommPorts();
        for (SerialPort sp : ports) {
            cbcom.getItems().add(sp.getSystemPortName());
        }
        cbcom.getSelectionModel().selectFirst();

        ComboBox<String> cbitv = (ComboBox) root.lookup("#combointerval");

        cbitv.getItems().add("읽지않음");
        for (int i = 1; i <= 30; i++) {
            cbitv.getItems().add("" + i);
        }
        cbitv.getSelectionModel().selectFirst();
        cbitv.setOnAction((e) -> {

            String selstr = (String) cbitv.getSelectionModel().getSelectedItem();
            if (selstr.contains("읽지않음") == true) {
                readThread.Stopthread();
                readThread = null;
            } else {
                int inum = Integer.parseInt(selstr);
                if (readThread == null) {
                    readThread = new ReadSensorThread(mIntergatedNode, mPanesensorlist);
                    readThread.StartThread();
                }
                readThread.timeintervalmsec = inum * 1000;
            }

        });


        AnchorPane pctl = (AnchorPane) root.lookup("#pane_control");
        Button btndev = (Button) root.lookup("#button_device");
        btndev.setOnAction((event) -> {
            if (DeviceScan() == true) {
                pctl.setDisable(false);
                cbitv.setDisable(false);
            }
        });

        Button btnnode = (Button) root.lookup("#button_scan");
        btnnode.setOnAction((event) -> {
            if (NodeScan() == true) {
                btndev.setDisable(false);
            }
        });

        ((Button) root.lookup("#btnopen")).setOnAction((event) -> {

            if (OpenPort((String) cbcom.getSelectionModel().getSelectedItem()) == true) {
                btnnode.setDisable(false);
            }
        });

        ComboBox<String> cbcontrol = (ComboBox) root.lookup("#cb_control");
        cbcontrol.getItems().add("로컬제어");
        cbcontrol.getItems().add("원격제어");
        cbcontrol.getSelectionModel().selectFirst();

        comboBox_start = (ComboBox) root.lookup("#cb_start");
        comboBox_end = (ComboBox) root.lookup("#cb_end");


        for (int i = 1; i <= 16; i++) {
            String cs = "" + i + "구역";
            comboBox_start.getItems().add(cs);
            comboBox_end.getItems().add(cs);
        }

        comboBox_start.getSelectionModel().selectFirst();
        comboBox_end.getSelectionModel().selectFirst();

        ((Button) root.lookup("#btn_node_control")).setOnAction((event) -> {

            String selstr = (String) cbcontrol.getSelectionModel().getSelectedItem();

            if (selstr.contains("로컬제어") == true) {
                mIntergatedNode.controlNode(200, NODECONTROL_COMMAND.OPERATION_LOCAL);
            } else {
                mIntergatedNode.controlNode(300, NODECONTROL_COMMAND.OPERATION_REMOTE);
            }
        });

        Label lbnodestatus = (Label) root.lookup("#lb_nodestaus");

        ((Button) root.lookup("#btn_read_nodestatus")).setOnAction((event) -> {

            if (mIntergatedNode.readNodeStatus() == true) {
                String cs = "";
                cs += "상태: " + KSX326xMetadata.GetStatusDescrition(STATUS_CODE.getEnum(mIntergatedNode.status))
                        + "\r\n";
                cs += "OPID: " + mIntergatedNode.opid + "\r\n";

                if (NODECONTROL_COMMAND.getEnum(mIntergatedNode.control) == NODECONTROL_COMMAND.OPERATION_LOCAL) {
                    cs += "제어권: 로컬제어";
                } else if (NODECONTROL_COMMAND
                        .getEnum(mIntergatedNode.control) == NODECONTROL_COMMAND.OPERATION_REMOTE) {
                    cs += "제어권: 원격제어";
                } else {
                    cs += "제어권: " + "지원하지않음." + "\r\n";
                }

                lbnodestatus.setText(cs);

            }
        });

        Label ntstatus = (Label) root.lookup("#lb_nt_status");
        ((Button) root.lookup("#btn_nt_read_status")).setOnAction((event) -> {

            String cs = "";
            NutrientDev mNt = mIntergatedNode.mNutrientDevices.get(0);
            if (mIntergatedNode.readDeviceStatus(mNt) == true) {
                cs += "OPID: " + mNt.opid + "\r\n";
                cs += "상태: " + KSX326xMetadata.GetStatusDescrition(STATUS_CODE.getEnum(mNt.status)) + "\r\n";
                cs += "관수구역: " + mNt.irrigation_area + "\r\n";
                cs += "경보정보: " + mNt.alert_information + "\r\n";
            } else {
                cs = " 제어상태 읽기 실패..";
            }

            ntstatus.setText(cs);

        });

        ToggleGroup group = new ToggleGroup();
        RadioButton rb1 = (RadioButton) root.lookup("#rb_level1");
        RadioButton rb2 = (RadioButton) root.lookup("#rb_level2");
        RadioButton rb3 = (RadioButton) root.lookup("#rb_level3");
        rb1.setToggleGroup(group);
        rb2.setToggleGroup(group);
        rb3.setToggleGroup(group);

        rb1.setSelected(true);

        group.selectedToggleProperty().addListener((obserableValue, old_toggle, new_toggle) -> {
            RadioButton rbsel = (RadioButton) group.getSelectedToggle();
            if (rbsel == rb1) {
                controllevel = 0;
                comboBox_start.setDisable(true);
                comboBox_end.setDisable(true);
                textBox_on_sec.setDisable(true);
                textBox_ec.setDisable(true);
                textBox_ph.setDisable(true);
            }
            if (rbsel == rb2) {
                controllevel = 1;
                comboBox_start.setDisable(false);
                comboBox_end.setDisable(false);
                textBox_on_sec.setDisable(false);
                textBox_ec.setDisable(true);
                textBox_ph.setDisable(true);
            }

            if (rbsel == rb3) {
                controllevel = 2;
                comboBox_start.setDisable(false);
                comboBox_end.setDisable(false);
                textBox_on_sec.setDisable(false);
                textBox_ec.setDisable(false);
                textBox_ph.setDisable(false);
            }

        });

        ((Button) root.lookup("#btn_nt_stop")).setOnAction((event) -> {
            mIntergatedNode.controlNutrient(mIntergatedNode.mNutrientDevices.get(0),
                    NUTRIENT_COMMAND.OPERATION_NUITRIENT_OFF, 0, 0, 0, 0, 0, 0);
        });

        ((Button) root.lookup("#btn_nt_on")).setOnAction((event) -> {
            netrient_on();
        });

        textBox_on_sec = (TextField) root.lookup("#tb_sec");

        textBox_ec = (TextField) root.lookup("#tb_ec");
        textBox_ph = (TextField) root.lookup("#tb_ph");

        stage.setScene(new Scene(root));
        stage.setTitle("양액기 모니터 JAVA");
        stage.show();



    }

    public boolean NodeScan() {
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

        if (mNodetype == PRODUCT_TYPE.INTEGRATEDNODE) {
            mIntergatedNode = new IntergratedNode(slaveid, mMaster);
            String cs = " 기본정보 읽기 실패.";
            if (mIntergatedNode.ReadNodeInformation() == true) {
                cs = " CertificateAuthority = " + mIntergatedNode.mNodeInfo.CertificateAuthority + "\r\n";
                cs += " CompanyCode = " + mIntergatedNode.mNodeInfo.CompanyCode + "\r\n";
                cs += " ProductType = " + mIntergatedNode.mNodeInfo.ProductType + "\r\n";
                cs += " ProductCode = " + mIntergatedNode.mNodeInfo.ProductCode + "\r\n";
                cs += " ChannelNumber = " + mIntergatedNode.mNodeInfo.ChannelNumber + "\r\n";
                cs += " ProtocolVersion = " + mIntergatedNode.mNodeInfo.ProtocolVersion + "\r\n";
            }

            lbinfo.setText(cs);
            return true;
        }
        return false;
    }

    public boolean DeviceScan() {
        try {
            if (mIntergatedNode.ReadDeviceCodeList() == true) {
                if (mIntergatedNode.CreateDevices() == true) {
                    System.out.println(" 디바이스갯수 = " + mIntergatedNode.mDevices.size());
                    for (SensorDev msdev : mIntergatedNode.mSensorDevices) {
                        PaneSensor ps = PaneSensor.Create(this, msdev);
                        sensorflowpane.getChildren().add(ps);
                        mPanesensorlist.add(ps);
                        ps.setOnMouseClicked((event) -> {
                            OnSensorPaneClick(ps);
                        });

                    }
                    return true;
                }

            }
        } catch (IOException e) {
                System.out.println("DeviceScan error : "+ e.toString());   
        }
        return false;

    }

    public boolean OpenPort(String pname) {
        if (mMaster.Open(pname, 9600) == false) {
            System.out.println("통신포트를 열수 없읍니다. ");
            return false;
        }

        return true;
    }

    public void OnSensorPaneClick(PaneSensor ps) {

    }

    private void netrient_on() {

        int mopid = 0;
        float mec = Float.parseFloat(textBox_ec.getText());
        float mph = Float.parseFloat(textBox_ph.getText());
        int msec = Integer.parseInt(textBox_on_sec.getText());
        int mstart = comboBox_start.getSelectionModel().getSelectedIndex() + 1;
        int mend = comboBox_end.getSelectionModel().getSelectedIndex() + 1;
        NutrientDev mND = mIntergatedNode.mNutrientDevices.get(0);
        NUTRIENT_COMMAND mcmd = NUTRIENT_COMMAND.OPERATION_NUITRIENT_OFF;

        if (controllevel == 0) {
            mcmd = NUTRIENT_COMMAND.OPERATION_NUITRIENT_ON;
            mopid = 11;
        } else if (controllevel == 1) {
            mcmd = NUTRIENT_COMMAND.OPERATION_NUITRIENT_AREA_ON;
            mopid = 22;
        } else if (controllevel == 2) {
            mcmd = NUTRIENT_COMMAND.OPERATION_NUITRIENT_PARAM_ON;
            mopid = 33;
        }

        if (mcmd != NUTRIENT_COMMAND.OPERATION_NUITRIENT_OFF) {
            mIntergatedNode.controlNutrient(mND, mcmd, mopid, mstart, mend, msec, mec, mph);
            // ReadNutrientStatus();
        }

    }

    public static void main(String[] args) {
        launch(args);
    }

}