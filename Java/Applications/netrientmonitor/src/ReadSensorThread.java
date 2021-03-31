import java.util.List;
import javafx.scene.paint.Color;


public class ReadSensorThread implements Runnable {

    private KSXNode mSensornode;
    private List<PaneSensor> mPlist;
    private boolean isrun = false;
    public int timeintervalmsec = 1000;

    public ReadSensorThread(KSXNode mnode, List<PaneSensor> mlist) {
        mSensornode = mnode;
        mPlist = mlist;
    }

    public void run() {
        isrun = true;
        while (isrun == true) {
            for (PaneSensor ps : mPlist) {
                ps.SetBKColor(Color.web("#defabb"));

                if (mSensornode.readDeviceStatus(ps.mySensor) == true) {
                    ps.UpdateStatus(false);
                }
                ThreadSleep(timeintervalmsec);
                ps.SetBKColor(Color.web("#61d800"));

                if (isrun == false) {
                    return;
                }
            }
        }
        System.out.println("ReadRunnable finish");
    }

    public void StartThread() {
        (new Thread(this)).start();
    }

    public void Stopthread() {
        isrun = false;
    }

    public void ThreadSleep(int msec) {
        try {
            Thread.sleep(msec);
        } catch (InterruptedException e) {
            System.out.println(e.toString());
        }
    }

}
