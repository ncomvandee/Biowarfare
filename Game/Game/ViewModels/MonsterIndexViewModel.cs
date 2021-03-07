using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;

using Game.Models;
using Game.Views;
using Game.GameRules;
using System.Threading.Tasks;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class MonsterIndexViewModel : BaseViewModel<MonsterModel>
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        // Monster Index View Model Instance 
        private static volatile MonsterIndexViewModel instance;
        // Syncroot Object 
        private static readonly object syncRoot = new Object();

        /// <summary>
        /// Returns Monster Index View Model Instance 
        /// </summary>
        public static MonsterIndexViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MonsterIndexViewModel();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        #region Constructor

        /// <summary>
        /// Constructor
        /// 
        /// The constructor subscribes message listeners for crudi operations
        /// </summary>
        public MonsterIndexViewModel()
        {
            Title = "Monster";

            #region Messages

            // Register the Create Message
            MessagingCenter.Subscribe<MonsterCellCreatePage, MonsterModel>(this, "Create", async (obj, data) =>
            {
                await CreateAsync(data as MonsterModel);
            });

            // Register the Update Message
            MessagingCenter.Subscribe<MonsterCellUpdatePage, MonsterModel>(this, "Update", async (obj, data) =>
            {
                // Have the Monster update itself
                data.Update(data);

                await UpdateAsync(data as MonsterModel);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<MonsterCellDeletePage, MonsterModel>(this, "Delete", async (obj, data) =>
            {
                await DeleteAsync(data as MonsterModel);
            });

            // Register the Set Data Source Message
            MessagingCenter.Subscribe<AboutPage, int>(this, "SetDataSource", async (obj, data) =>
            {
                await SetDataSource(data);
            });

            // Register the Wipe Data List Message
            MessagingCenter.Subscribe<AboutPage, bool>(this, "WipeDataList", async (obj, data) =>
            {
                await WipeDataListAsync();
            });

            #endregion Messages
        }


        #endregion Constructor

        #region DataOperations_CRUDi

        /// <summary>
        /// Returns the Monster passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override MonsterModel CheckIfExists(MonsterModel data)
        {
            if (data == null)
            {
                return null;
            }

            // This will walk the Monster and find if there is one that is the same.
            // If so, it returns the Monster.

            var myList = Dataset.Where(a =>
                                        a.Name == data.Name &&
                                        a.Description == data.Description 
                                        )
                                        .FirstOrDefault();

            if (myList == null)
            {
                // it's not a match, return false;
                return null;
            }

            return myList;
        }

        /// <summary>
        /// Load the Default Data
        /// </summary>
        /// <returns></returns>
        public override List<MonsterModel> GetDefaultData() 
        {
            return DefaultData.LoadData(new MonsterModel());
        }

        #endregion DataOperations_CRUDi

        #region SortDataSet

        /// <summary>
        /// The Sort Order for the MonsterModel
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public override List<MonsterModel> SortDataset(List<MonsterModel> dataset)
        {
            return dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ToList();
        }

        #endregion SortDataSet

        /// <summary>
        /// Return monster list without Cancer
        /// </summary>
        /// <returns></returns>
        public List<MonsterModel> GetMonstersListExceptBoss()
        {
            // List of monsters without cancer
            var ResultList = Dataset.Where(a => a.MonsterType.Equals(MonsterTypeEnum.Cancer) == false).ToList();

            return ResultList;
        }

        /// <summary>
        /// Get specific Monster
        /// </summary>
        /// <returns></returns>
        public MonsterModel GetSpecificMonster(MonsterTypeEnum MonsterType)
        {
            // Specific monster in dataset
            MonsterModel result = Dataset.Where(a => a.MonsterType.Equals(MonsterType)).FirstOrDefault();

            if (result == null)
            {
                return null;
            }
            return result;
        }

        /// <summary>
        /// Takes an Monster string ID and looks it up and returns the Monster
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MonsterModel GetMonster(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            // Monster Data
            MonsterModel myData = Dataset.Where(a => a.Id.Equals(id)).FirstOrDefault();
            if (myData == null)
            {
                return null;
            }

            return myData;
        }
        
    }
}