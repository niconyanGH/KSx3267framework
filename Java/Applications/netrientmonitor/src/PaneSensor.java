import java.io.IOException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.control.Label;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.Pane;
import javafx.scene.paint.Color;
import javafx.scene.chart.XYChart;


public class PaneSensor extends Pane {
    public SensorDev mySensor;
    private Label lbcname;
    private Label lbcvalue;
    private Label lbcstatus;
    public XYChart.Series<String, Double> mSeries = new XYChart.Series<String, Double>();

    public static PaneSensor Create(Application mcls, SensorDev ms) throws IOException {
        Parent pp = FXMLLoader.load(mcls.getClass().getResource("panesensor.fxml"));
        return new PaneSensor(pp, ms);
    }

    public PaneSensor(Parent pp, SensorDev ms) {
        super(pp);
        mySensor = ms;
        lbcname = (Label) pp.lookup("#lbname");
        lbcvalue = (Label) pp.lookup("#lbvalue");
        lbcstatus = (Label) pp.lookup("#lbstatus");
        UpdateStatus(false);
        this.SetBKColor(Color.web("#61d800"));
    }

    public void UpdateStatus(boolean isvaluesave) {
        Platform.runLater(() -> {
            try {
                if (isvaluesave == true) {
                    
                    mSeries.getData()
                            .add(new XYChart.Data(LocalDateTime.now().format(DateTimeFormatter.ofPattern("HH:mm:ss")), mySensor.value));
                    System.out.println("mSeries = " + mSeries.getData().size());
                }
                lbcname.setText(mySensor.mDevice.Name);
                lbcvalue.setText(mySensor.GetValuestring(true));
                lbcstatus.setText("상태: " + KSX326xMetadata.GetStatusDescrition(STATUS_CODE.getEnum(mySensor.status)));
            } catch (Exception ex) {
                System.out.println(ex.toString());
            }
        });

    }

    public void SetBKColor(Color mColor) {
        this.setBackground(new Background(new BackgroundFill(mColor, null, null)));
    }

}
