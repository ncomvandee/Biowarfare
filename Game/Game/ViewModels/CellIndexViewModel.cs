﻿using System;
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
    public class CellIndexViewModel : BaseViewModel<CharacterModel>
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        // Cell Index View Model Instance 
        private static volatile CellIndexViewModel instance;
        //Syncroot Object 
        private static readonly object syncRoot = new Object();

        /// <summary>
        /// Create Cell Index View Model instance 
        /// </summary>
        public static CellIndexViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new CellIndexViewModel();
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
        public CellIndexViewModel()
        {
            Title = "Cell";

            #region Messages

            // Register the Create Message
            MessagingCenter.Subscribe<CellCreatePage, CharacterModel>(this, "Create", async (obj, data) =>
            {
                await CreateAsync(data as CharacterModel);
            });

            // Register the Update Message
            MessagingCenter.Subscribe<CellUpdatePage, CharacterModel>(this, "Update", async (obj, data) =>
            {
                // Have the Cell update itself
                data.Update(data);

                await UpdateAsync(data as CharacterModel);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<CellDeletePage, CharacterModel>(this, "Delete", async (obj, data) =>
            {
                await DeleteAsync(data as CharacterModel);
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
        /// Returns the Cell passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override CharacterModel CheckIfExists(CharacterModel data)
        {
            if (data == null)
            {
                return null;
            }

            // This will walk the Cell and find if there is one that is the same.
            // If so, it returns the Cell.

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
        public override List<CharacterModel> GetDefaultData() 
        {
            return DefaultData.LoadData(new CharacterModel());
        }

        #endregion DataOperations_CRUDi

        #region SortDataSet

        /// <summary>
        /// The Sort Order for the CharacterModel
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public override List<CharacterModel> SortDataset(List<CharacterModel> dataset)
        {
            return dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ToList();
        }

        #endregion SortDataSet

        /// <summary>
        /// Takes an Cell string ID and looks it up and returns the Cell
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CharacterModel GetCell(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            // Cell Data
            CharacterModel myData = Dataset.Where(a => a.Id.Equals(id)).FirstOrDefault();
            if (myData == null)
            {
                return null;
            }

            return myData;
        }
        
    }
}