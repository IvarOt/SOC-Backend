using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC_backend.logic.Models.Player;

namespace SOC_backend.test.TestObjects
{
    public class PlayerObjects
    {
        public Player testPlayer {  get; set; }
        public PlayerObjects()
        {
            testPlayer = new Player(1, "test", "test@gmail.com","Test123!");
        }
    }
}
