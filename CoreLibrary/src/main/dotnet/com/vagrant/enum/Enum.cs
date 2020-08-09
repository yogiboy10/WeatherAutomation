namespace Enum
{
    public enum LocatorType
    {
        ID,
        XPATH,
        NAME,
        CSS,
        CLASS,
        LINK_TEXT,
        PARTIAL_LINK_TEXT
    }

    public enum DropdownType
    {
        VALUE,
        INDEX,
        TEXT
    }

    public enum DriverType
    {
        CHROME, WINIUM, REMOTE
    }

    public enum Outcome
    {
        PASS=2,FAIL=3,BLOCK=7,NOT_APPLICABLE=11
    }

    public enum FileType {JSON,XML}
    //  public enum ExecutionModeType{DEFAULT,SCENARIO}
}
