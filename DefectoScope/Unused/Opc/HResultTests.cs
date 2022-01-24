using TitaniumAS.Opc.Client.Common;

namespace DefectoScope
{
    
    public class HResultTests
    {
        
        public void It_Should_Create_Correct_Itf_Error_Code()
        {
            var _ = HRESULT.AddItfError(0x0200, "My error");
        }

        
        public void It_Should_Add_New_Itf_Error_To_The_Dictionary()
        {
            var _ = HRESULT.AddItfError(0x0200, "My error");
        }

        
        public void It_Should_Throw_Argument_Out_Of_Range_Exception_When_A_Code_Less_Than_0x0200()
        {
            HRESULT.AddItfError(0x01FF, "My error");
        }

        
        public void It_Should_Throw_Argument_Out_Of_Range_Exception_When_A_Code_Greater_Than_0xFFFF()
        {
            HRESULT.AddItfError(0x10000, "My error");
        }
    }
}