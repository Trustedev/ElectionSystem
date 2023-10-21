using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionSystem
{
    
    internal class VoteScanner
    {
        List<Party> partyList = new List<Party>(); // parti listesi for döngüsünde kullanılacak            
        List<City> cityList = new List<City>();        
        int currentLine = 1;
       
        int phaseChangedLine; // parti listesi taramasının bittiği satırı kayıt altına almak için kullanacağız.
        byte scanPhase = 0; // taramanın hangi fazda olduğunu tutuyor
        public void scanVotes(string filePath)
        {            
            var lines = File.ReadAllLines(filePath);
            int partyCount = int.Parse(lines[0]);
            int cityCount;

            for (var i = 1; i < partyCount + 1; i++) // ilk satırdan parti sayısını aldığımızdan i = 1 ile başlıyoruz
            {
                partyList.Add(new Party());
                partyList[i - 1].partyName = lines[i]; // i-1 yapmazsak liste kayıyor ve partyList[0] yerine [1] e erişiyor. 
                partyList[i - 1].partyID = i;
                currentLine++;
            }

            cityCount = int.Parse(lines[currentLine]); // şu anki satır şehir sayısını temsil ediyor
            currentLine++; // bir sonraki satırdan başlaması gerek, istediğimiz veriyi zaten aldık

            for (var i = 0; i < cityCount; i = i) // ilk satırdan parti sayısını aldığımızdan i = 1 ile başlıyoruz
            {

                switch (scanPhase)
                {
                    case 0:
                        cityList.Add(new City());
                        cityList[i].plateID = int.Parse(lines[currentLine]);                       
                        currentLine++;
                        scanPhase = 1;
                        break;

                    case 1:
                        cityList[i].deputyLimit = int.Parse(lines[currentLine]);                        
                        currentLine++;
                        scanPhase = 2;
                        break;

                    case 2:
                        for (int j = 0; j < partyCount; j++)
                        {
                            cityList[i].votes.Add(int.Parse(lines[currentLine]));
                            partyList[j].cityVotes.Add(cityList[i].plateID, int.Parse(lines[currentLine])); // plaka ID'si + parti id bizim tuttuğumuz özel veridir. parti id çıkarılırsa yeniden plaka id bulunur                           
                            currentLine++;
                            scanPhase = 0;

                        }
                        i++;
                        break;

                }

            }
          
        }
        
        public string getPartyName(int partyID)
        {
           return partyList[partyID - 1].partyName;
            
        }
        public int getVote(int partyID, int cityID)
        {                    
            return cityList[cityID].votes[partyID];
        }

        public int getVoteTotal(int partyID)
        {
            int totalVote = 0;
            for(int i = 1; i < cityList.Count+1; i++)
            {
                totalVote += partyList[partyID].cityVotes[i];
            }
            return totalVote;
            
        }
        
        public void printVoteTable()
        {
            Console.WriteLine("Turkiye Geneli");
            Console.WriteLine("Katılan Parti Sayısı: " + partyList.Count);
            Console.WriteLine("\nPARTI ADI".PadRight(20) + "OY SAYISI".PadRight(20) + "TEST");
            Console.WriteLine(new string('-', 10) + new string(' ', 9) + new string('-', 9) + new string(' ', 13) + new string('-', 8));

            for (int i = 0; i < partyList.Count; i++)
            {
                Console.WriteLine(partyList[i].partyName.PadRight(20) + getVoteTotal(i));
                
            }
            

        }

    }
}
