namespace CiriumTest.Business.Interface
{
    public interface IAviationRepository
    {   
        /// <summary>
        /// Used to save user input into variable for calculation
        /// </summary>
        /// <param name="aircraftType">Airbus or Boeing</param>
        /// <param name="minutes">Minutes passed in the input with aircraft</param>
        void SetAviationData(string aircraftType, decimal minutes);

        /// <summary>
        /// Used to calculate Hours and minutes based on details provided in input
        /// </summary>
        /// <returns>Calcuate minutes in Hours:Minutes</returns>
        string Calculate();
    }
}
