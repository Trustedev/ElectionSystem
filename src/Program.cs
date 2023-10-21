namespace ElectionSystem
{    
    internal class Program
    {



        static void Main(string[] args)
        {

            VoteScanner scan = new VoteScanner();
            scan.scanVotes("secim.txt");            
            scan.printVoteTable();    

           

        }
    }
}