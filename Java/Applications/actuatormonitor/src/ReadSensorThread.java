import java.util.List;
import javafx.scene.paint.Color;

public class ReadSensorThread implements Runnable {
        private ActuatorNode mActuatorNode;
        private List<PaneSensor> mPlist;
        private boolean isrun=false;
        public int timeintervalmsec=500;
        public PaneSensor prePaneSensor1;
        public PaneSensor prePaneSensor2;
        public ReadSensorThread(ActuatorNode mNode, List<PaneSensor> mList)
        {
            mActuatorNode = mNode;
            mPlist = mList;
        }

        public void run() 
        {
            isrun=true;
            
            while(isrun==true)
            {
                for(PaneSensor ps : mPlist)
                {
                    ps.SetBKColor(Color.valueOf("#E0F7FA"));                    // read color
                    if(mActuatorNode.readDeviceStatus(ps.myActuator)== true)
                    {
                        ps.UpdateStatus(true);
                    }
                    ThreadSleep(timeintervalmsec);
                    if(prePaneSensor1==ps){
                        ps.SetBKColor(Color.valueOf("#26A69A"));                // select color
                    } else if (prePaneSensor2==ps){
                        ps.SetBKColor(Color.valueOf("#26A69A"));                // select color
                    } else{
                        ps.SetBKColor(Color.valueOf("#80CBC4"));                // default color
                    }

                    if(isrun==false)
                    {
                        return ;
                    }
                }
            }
            System.out.println("ReadRunnable finish");
        }
    
        public void StartThread() 
        {
            (new Thread(this)).start();
        }
        public void Stopthread()
        {
            isrun=false;
        }
        public void Clearthread()
        {
            mPlist.clear();
        }
        public void ThreadSleep(int msec)
        {
            try
            {
                Thread.sleep(msec);
            }
            catch(InterruptedException e)
            {
                System.out.println(e.toString());
            }
        }
    }

