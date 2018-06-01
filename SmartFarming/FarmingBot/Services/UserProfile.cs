using System;

namespace FarmingBot.Services
{
    [Serializable]
    public class UserProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CropCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CropSubCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SearchOption { get; set; }


        /// <summary>
        /// To store user queries
        /// </summary>
        public string Query { get; set; }
        
    }
}