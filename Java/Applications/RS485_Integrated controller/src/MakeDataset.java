import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.StringProperty;

public class MakeDataset {
    
    private StringProperty Address ;
    private StringProperty Description;
    private StringProperty DecimalString;
    private StringProperty HexaString;
    private StringProperty FloatString;
    
    public MakeDataset(String[] mstrlist )
    {
        Address= new SimpleStringProperty(mstrlist[0]);
        Description= new SimpleStringProperty(mstrlist[1]);
        DecimalString= new SimpleStringProperty(mstrlist[2]);
        HexaString= new SimpleStringProperty(mstrlist[3]);
        FloatString= new SimpleStringProperty(mstrlist[4]);
    }

    public StringProperty AddressProperty(){
        return Address;
    }
    public StringProperty DescriptionProperty(){
        return Description;
    }
    public StringProperty DecimalStringProperty(){
        return DecimalString;
    }
    public StringProperty HexaStringProperty(){
        return HexaString;
    }
    public StringProperty FloatStringProperty(){
        return FloatString;
    }
    public class RegData {
    }
}
