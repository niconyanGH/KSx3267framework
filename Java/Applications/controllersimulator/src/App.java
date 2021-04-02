import java.io.IOException;

import com.fazecast.jSerialComm.SerialPort;

import javafx.application.Application;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control. *;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;

public class App extends Application {

    private ComboBox cbportList;
    private Button btnOpen;

    private Pane paneModeSelector;
    private RadioButton rbtnSensorNodeReading;
    private RadioButton rbtnActuatorNodeReading;
    private RadioButton rbtnActuatorNodeWritng;
    private ComboBox cmboxFunctionSelector;
    private Label lblLength;
    private ComboBox cmboxCommandSelector;
    private TextField txtFSlaveAddress;
    private TextField txtFStartAddress;
    private TextField txtFLength;
    private Button btnSend;

    private TextArea txtACommand;

    private TableView tblVResultTable;
    private ObservableList<MakeDataset> deviceData = FXCollections.observableArrayList();

    public Button btnDvc;

    STDModbusMaster mMaster = new STDModbusMaster();
    STDModbusResponse mResponse;

    String defaultPaneColor = "#7986CB";
    String selectedPaneColor = "#BA68C8";
    String scaningPaneColor = "#CDDC39";

    String cmd1 = "값 읽기[03]";
    String cmd2 = "값 한개 쓰기[06]";
    
    int cntcmd = 1;
    int operationMode = 0;

    @Override public void start(Stage stage)throws IOException {

        Parent root = FXMLLoader.load(getClass().getResource("mainFrame.fxml"));

        stage.setTitle("제어기 모의실험 앱 KD");
        cbportList = (ComboBox)root.lookup("#comboportList");
        btnOpen = (Button)root.lookup("#btnopen");

        paneModeSelector = (Pane)root.lookup("#pane_ModeSelector");
        cmboxFunctionSelector = (ComboBox)root.lookup("#cmbox_FunctionSelector");
        rbtnSensorNodeReading = (RadioButton)root.lookup("#rbtn_SensorNodeReading");
        rbtnActuatorNodeReading = (RadioButton)root.lookup("#rbtn_ActuatorNodeReading");
        rbtnActuatorNodeWritng = (RadioButton)root.lookup("#rbtn_ActuatorNodeWriting");
        lblLength = (Label)root.lookup("#lbl_Length");
        txtFSlaveAddress = (TextField)root.lookup("#txtF_SlaveAddress");
        cmboxCommandSelector = (ComboBox)root.lookup("#cmbox_Command");
        txtFStartAddress = (TextField)root.lookup("#txtF_StartAddress");
        txtFLength = (TextField)root.lookup("#txtF_Length");
        btnSend = (Button)root.lookup("#btn_Send");
        
        txtACommand = (TextArea)root.lookup("#txtA_CommandLineInterface");

        tblVResultTable = (TableView)root.lookup("#tblV_ResultTable");
        
        ((TableColumn)tblVResultTable.getColumns().get(0)).setCellValueFactory(new PropertyValueFactory("Address"));
        ((TableColumn)tblVResultTable.getColumns().get(1)).setCellValueFactory(new PropertyValueFactory("Description"));
        ((TableColumn)tblVResultTable.getColumns().get(2)).setCellValueFactory(new PropertyValueFactory("DecimalString"));
        ((TableColumn)tblVResultTable.getColumns().get(3)).setCellValueFactory(new PropertyValueFactory("HexaString"));
        ((TableColumn)tblVResultTable.getColumns().get(4)).setCellValueFactory(new PropertyValueFactory("FloatString"));

        SerialPort[] ports = SerialPort.getCommPorts();
        paneModeSelector.setDisable(true);
        for (SerialPort pp : ports) {
            cbportList.getItems().add(pp.getDescriptivePortName());
        }
        
        
        rbtnSensorNodeReading.setOnAction((event) -> {
            setOperationMode(rbtnSensorNodeReading);
            setCommand();
            operationMode = 1;
        });
        rbtnActuatorNodeReading.setOnAction((event) -> {
            setOperationMode(rbtnActuatorNodeReading);
            setCommand();
            operationMode = 2;
        });
        rbtnActuatorNodeWritng.setOnAction((event) -> {
            setOperationMode(rbtnActuatorNodeWritng);
            setCommand();
            operationMode = 3;
        });
        
        cmboxFunctionSelector.setOnAction((event) -> {
            setCommand();
        });

        btnOpen.setOnAction((event) -> {
            System.out.println(cbportList.getSelectionModel().getSelectedItem());
            for (SerialPort pp : ports) {
                if (pp.getDescriptivePortName() == cbportList.getSelectionModel().getSelectedItem()) 
                    OpenPort(pp.getSystemPortName());
                }
            });

        btnSend.setOnAction((event) -> {
            int slaveAddr = Integer.parseInt(txtFSlaveAddress.getText());
            int startAddr = Integer.parseInt(txtFStartAddress.getText());
            int msgLength = Integer.parseInt(txtFLength.getText());
            String[] ms=new String[5];
            int cntAddr = 400000 + Integer.parseInt(txtFStartAddress.getText());
            byte[] msgValue = new byte[2];
            ByteUtil.cast_value_to_bytes_insert_buffer(Short.parseShort(txtFLength.getText()), msgValue, 0);
            tblVResultTable.getItems().clear();

            txtACommand.appendText(Integer.toString(cntcmd)+". ");
            if(cmboxCommandSelector.getSelectionModel().getSelectedItem().toString().equals(cmd1))
            {
                mResponse = mMaster.StandardManualWordRead_F3(slaveAddr, startAddr, msgLength, 1000);
                
                
                txtACommand.appendText(Integer.toString(mResponse.byte_length));
                txtACommand.appendText("byte 메세지 수신.");
                txtACommand.appendText("\n");
                
                for(int readData : mResponse.wordDatas)
                {
                    ms[0] = Integer.toString(cntAddr);
                    ms[1] = addrMapDes.mapping(operationMode,cntAddr);
                    ms[2] = Integer.toString(readData);
                    ms[3] = Integer.toHexString(readData);
                    ms[4] = "0.00";
                    deviceData.add(new MakeDataset(ms));
                    
                    cntAddr++;
                }
                
                tblVResultTable.setItems(deviceData);
            } else if(cmboxCommandSelector.getValue().toString().equals(cmd2))
            {
                mResponse = mMaster.StandardManualWordWrite_F10(slaveAddr, startAddr, msgValue, 1000);

                txtACommand.appendText(Integer.toString(mResponse.byte_length));
                txtACommand.appendText("byte 메세지 수신.");
                txtACommand.appendText("\n");

                for(int readData : mResponse.wordDatas)
                {
                    ms[0] = Integer.toString(cntAddr);
                    ms[1] = addrMapDes.mapping(operationMode,cntAddr);
                    ms[2] = Integer.toString(readData);
                    ms[3] = Integer.toHexString(readData);
                    ms[4] = "0.00";
                    deviceData.add(new MakeDataset(ms));
                    
                    cntAddr++;
                }
                tblVResultTable.setItems(deviceData);
            }
            cntcmd++;
        });
        stage.setScene(new Scene(root));
        stage.show();
    }

    public void OpenPort(String tmpPort) {
        if (mMaster.Open(tmpPort, 9600) == false) {
            System.out.println("통신포트를 열수 없읍니다. ");
        } else {
            cbportList.setDisable(true);
            btnOpen.setDisable(true);
            paneModeSelector.setDisable(false);
            
        rbtnSensorNodeReading.setSelected(true);
        rbtnSensorNodeReading.requestFocus();
        setOperationMode(rbtnSensorNodeReading);
        }
    }

    public void setOperationMode(RadioButton mode){
        cmboxFunctionSelector.getItems().clear();
        cmboxCommandSelector.getItems().clear();
        if(mode == rbtnSensorNodeReading){
            cmboxFunctionSelector.setVisible(true);
            cmboxFunctionSelector.getItems().add("센서노드 정보");
            cmboxFunctionSelector.getItems().add("센서장치 코드");
            cmboxFunctionSelector.getItems().add("센서 상태");
            cmboxCommandSelector.getItems().add(cmd1);
        } else if(mode == rbtnActuatorNodeReading){
            cmboxFunctionSelector.setVisible(true);
            cmboxFunctionSelector.getItems().add("구동기노드 정보");
            cmboxFunctionSelector.getItems().add("구동기장치 코드");
            cmboxFunctionSelector.getItems().add("스위치 상태");
            cmboxFunctionSelector.getItems().add("스위치 명령");
            cmboxCommandSelector.getItems().add(cmd1);
        } else if(mode == rbtnActuatorNodeWritng){
            cmboxFunctionSelector.setVisible(false);
            cmboxCommandSelector.getItems().add(cmd2);
        }
        cmboxFunctionSelector.getSelectionModel().selectFirst();
        cmboxCommandSelector.getSelectionModel().selectFirst();
    }

    public void setCommand(){
        txtFSlaveAddress.setText("1");
        if(rbtnActuatorNodeWritng.isSelected() == false){
            Object aa=cmboxFunctionSelector.getSelectionModel().getSelectedItem();
            if(aa!=null){
            switch(aa.toString()){
                case "센서노드 정보":
                case "구동기노드 정보":
                txtFStartAddress.setText("1");
                txtFLength.setText("8");
                break;
                case "센서장치 코드":
                txtFStartAddress.setText("101");
                txtFLength.setText("30");
                break;
                case "센서 상태":
                txtFStartAddress.setText("202");
                txtFLength.setText("90");
                break;
                case "구동기장치 코드":
                txtFStartAddress.setText("101");
                txtFLength.setText("24");
                break;
                case "스위치 상태":
                txtFStartAddress.setText("201");
                txtFLength.setText("97");
                break;
                case "스위치 명령":
                txtFStartAddress.setText("501");
                txtFLength.setText("97");
                break;
                default:
                System.out.println("기능선택오류입니다.");
                break;
            }
        }
        } else {
            txtFStartAddress.setText("1");
            lblLength.setText("Value");
            txtFLength.setText("0");
        }
    }

    public static void main(String[] args) {
        launch(args);
    }
}
