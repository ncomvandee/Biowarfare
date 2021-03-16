using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineModels;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class BattlePage : ContentPage
    {
        // HTML Formatting for message output box
        public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        // Wait time before proceeding
        public int WaitTime = 1500;

        // Hold the Map Objects, for easy access to update them
        public Dictionary<string, object> MapLocationObject = new Dictionary<string, object>();
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        // Empty Constructor for UTs
        bool UnitTestSetting;
        public BattlePage(bool UnitTest) { UnitTestSetting = UnitTest; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage()
        {
            InitializeComponent();

            // Set initial State to Starting
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Starting;

            // Set up the UI to Defaults
            BindingContext = BattleEngineViewModel.Instance;

            // Create and Draw the Map
            InitializeMapGrid();

            // Start the Battle Engine
            BattleEngineViewModel.Instance.Engine.StartBattle(false);

            // Populate the UI Map
            DrawMapGridInitialState();

            // Ask the Game engine to select who goes first
            BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);

            // Add Players to Display
            DrawGameAttackerDefenderBoard();

            // Set the Battle Mode
            ShowBattleMode();
        }

        /// <summary>
        /// Dray the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {
            var CharacterBoxList = CharacterBox.Children.ToList();
            foreach (var data in CharacterBoxList)
            {
                CharacterBox.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                CharacterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            var MonsterBoxList = MonsterBox.Children.ToList();
            foreach (var data in MonsterBoxList)
            {
                MonsterBox.Children.Remove(data);
            }

            // Draw the Monsters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
            {
                MonsterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            // Add one black PlayerInfoDisplayBox to hold space in case the list is empty
            CharacterBox.Children.Add(PlayerInfoDisplayBox(null));

            // Add one black PlayerInfoDisplayBox to hold space incase the list is empty
            MonsterBox.Children.Add(PlayerInfoDisplayBox(null));

        }

        /// <summary>
        /// Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = ""
                };
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["PlayerBattleMediumStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerBattleDisplayBox"],
                BackgroundColor = Color.Transparent,
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }

        #region BattleMapMode

        /// <summary>
        /// Create the Initial Map Grid
        /// 
        /// All lcoations are empty
        /// </summary>
        /// <returns></returns>
        public bool InitializeMapGrid()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.ClearMapGrid();

            return true;
        }

        /// <summary>
        /// Draw the Map Grid
        /// Add the Players to the Map
        /// 
        /// Need to have Players in the Engine first, to then put on the Map
        /// 
        /// The Map Objects are all created with the map background image first
        /// 
        /// Then the actual characters are added to the map
        /// </summary>
        public void DrawMapGridInitialState()
        {
            // Create the Map in the Game Engine
            BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            CreateMapGridObjects();

            UpdateMapGrid();
        }

        /// <summary>
        /// Walk the current grid
        /// check each cell to see if it matches the engine map
        /// Update only those that need change
        /// </summary>
        /// <returns></returns>
        public bool UpdateMapGrid()
        {
            // Disable the map grid
            MapGrid.IsEnabled = false;
            UseAbilityButton.IsVisible = false;
            UseItemButton.IsVisible = false;

            var cell = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();
            var currentAttacker = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            // If the current turn is cell, enable the map grid
            if (cell != null && cell.PlayerType == PlayerTypeEnum.Character)
            {
                MapGrid.IsEnabled = true;
                UseAbilityButton.IsVisible = true;
                UseItemButton.IsVisible = true;
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                // Use the ImageButton from the dictionary because that represents the player object
                object MapObject = GetMapGridObject(GetDictionaryImageButtonName(data));
                if (MapObject == null)
                {
                    return false;
                }

                var imageObject = (ImageButton)MapObject;

                // Check automation ID on the Image, That should match the Player, if not a match, the cell is now different need to update
                if (!imageObject.AutomationId.Equals(data.Player.Guid))
                {
                    // The Image is different, so need to re-create the Image Object and add it to the Stack
                    // That way the correct monster is in the box.

                    MapObject = GetMapGridObject(GetDictionaryStackName(data));
                    if (MapObject == null)
                    {
                        return false;
                    }

/*                    var stackObject = (StackLayout)MapObject;

                    // Remove the ImageButton
                    stackObject.Children.RemoveAt(0);

                    var PlayerImageButton = DetermineMapImageButton(data);

                    stackObject.Children.Add(PlayerImageButton);

                    // Update the Image in the Datastructure
                    MapGridObjectAddImage(PlayerImageButton, data);

                    stackObject.BackgroundColor = Color.Transparent;*/
                    //stackObject.BackgroundColor = DetermineMapBackgroundColor(data);

                    AddImageButtonToMap(MapObject, data, Color.Transparent);
                }

                //Highline the next attacker's turn
                if(data.Player == cell)
                {
                    MapObject = GetMapGridObject(GetDictionaryStackName(data));
                    if (MapObject == null)
                    {
                        return false;
                    }

/*                    var stackObject = (StackLayout)MapObject;

                    // Remove the ImageButton
                    stackObject.Children.RemoveAt(0);

                    var PlayerImageButton = DetermineMapImageButton(data);

                    stackObject.Children.Add(PlayerImageButton);

                    // Update the Image in the Datastructure
                    MapGridObjectAddImage(PlayerImageButton, data);

                    stackObject.BackgroundColor = Color.Yellow;*/

                    AddImageButtonToMap(MapObject, data, Color.Yellow);
                }
                //Highline the next attacker's turn
                if (data.Player == currentAttacker)
                {
                    MapObject = GetMapGridObject(GetDictionaryStackName(data));
                    if (MapObject == null)
                    {
                        return false;
                    }

/*                    var stackObject = (StackLayout)MapObject;

                    // Remove the ImageButton
                    stackObject.Children.RemoveAt(0);

                    var PlayerImageButton = DetermineMapImageButton(data);

                    stackObject.Children.Add(PlayerImageButton);

                    // Update the Image in the Datastructure
                    MapGridObjectAddImage(PlayerImageButton, data);

                    stackObject.BackgroundColor = Color.Transparent;*/

                    AddImageButtonToMap(MapObject, data, Color.Transparent);
                }

            }

            return true;
        }

        /// <summary>
        /// Change back ground color base on current attacker and next character turn
        /// </summary>
        /// <param name="MapObject"></param>
        /// <param name="data"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool AddImageButtonToMap(object MapObject, MapModelLocation data ,Color color)
        {


            var stackObject = (StackLayout)MapObject;

            // Remove the ImageButton
            stackObject.Children.RemoveAt(0);

            var PlayerImageButton = DetermineMapImageButton(data);

            stackObject.Children.Add(PlayerImageButton);

            // Update the Image in the Datastructure
            MapGridObjectAddImage(PlayerImageButton, data);

            stackObject.BackgroundColor = color;
            return true;
        }

        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryFrameName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Frame", data.Row, data.Column);
        }

        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryStackName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Stack", data.Row, data.Column);
        }

        /// <summary>
        /// Covert the player map location to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryImageButtonName(MapModelLocation data)
        {
            // Look up the Frame in the Dictionary
            return string.Format("MapR{0}C{1}ImageButton", data.Row, data.Column);
        }

        /// <summary>
        /// Populate the Map
        /// 
        /// For each map position in the Engine
        /// Create a grid object to hold the Stack for that grid cell.
        /// </summary>
        /// <returns></returns>
        public bool CreateMapGridObjects()
        {
            // Make a frame for each location on the map
            // Populate it with a new Frame Object that is unique
            // Then updating will be easier

            foreach (var location in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                var data = MakeMapGridBox(location);

                // Add the Box to the UI

                MapGrid.Children.Add(data, location.Column, location.Row);
            }

            // Set the Height for the MapGrid based on the number of rows * the height of the BattleMapFrame

            var height = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapXAxiesCount * 60;

            BattleMapDisplay.MinimumHeightRequest = height;
            BattleMapDisplay.HeightRequest = height;

            return true;
        }

        /// <summary>
        /// Get the Frame from the Dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetMapGridObject(string name)
        {
            MapLocationObject.TryGetValue(name, out object data);
            return data;
        }

        /// <summary>
        /// Make the Game Map Frame 
        /// Place the Character or Monster on the frame
        /// If empty, place Empty
        /// </summary>
        /// <param name="mapLocationModel"></param>
        /// <returns></returns>
        public Frame MakeMapGridBox(MapModelLocation mapLocationModel)
        {
            if (mapLocationModel.Player == null)
            {
                mapLocationModel.Player = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare;
            }

            var PlayerImageButton = DetermineMapImageButton(mapLocationModel);


            var PlayerStack = new StackLayout
            {
                Padding = 0,
                Style = (Style)Application.Current.Resources["BattleMapImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                //BackgroundColor = DetermineMapBackgroundColor(mapLocationModel),
                Children = {
                    PlayerImageButton
                },
            };

            MapGridObjectAddImage(PlayerImageButton, mapLocationModel);
            MapGridObjectAddStack(PlayerStack, mapLocationModel);

            var MapFrame = new Frame
            {
                Style = (Style)Application.Current.Resources["BattleMapFrame"],
                BackgroundColor = Color.FromHex("5A0746"),
                BorderColor = Color.FromHex("6D1F27"),
                Content = PlayerStack,
                AutomationId = GetDictionaryFrameName(mapLocationModel)
            };

            return MapFrame;
        }

        /// <summary>
        /// This add the ImageButton to the stack to kep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddImage(ImageButton data, MapModelLocation MapModel)
        {
            var name = GetDictionaryImageButtonName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);

            return true;
        }

        /// <summary>
        /// This adds the Stack into the Dictionary to keep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddStack(StackLayout data, MapModelLocation MapModel)
        {
            var name = GetDictionaryStackName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);
            return true;
        }

        /// <summary>
        /// Set the Image onto the map
        /// The Image represents the player
        /// 
        /// So a charcter is the character Image for that character
        /// 
        /// The Automation ID equals the guid for the player
        /// This makes it easier to identify when checking the map to update thigns
        /// 
        /// The button action is set per the type, so Characters events are differnt than monster events
        /// </summary>
        /// <param name="MapLocationModel"></param>
        /// <returns></returns>
        public ImageButton DetermineMapImageButton(MapModelLocation MapLocationModel)
        {
            var data = new ImageButton
            {
                Style = (Style)Application.Current.Resources["BattleMapPlayerSmallStyle"],
                Source = MapLocationModel.Player.ImageURI,

                // Store the guid to identify this button
                AutomationId = MapLocationModel.Player.Guid
            };


            switch (MapLocationModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    data.Clicked += (sender, args) => SetSelectedCharacter(MapLocationModel);
                    break;
                case PlayerTypeEnum.Monster:
                    data.Clicked += (sender, args) => SetSelectedMonster(MapLocationModel);
                    break;
                case PlayerTypeEnum.Unknown:
                    data.Clicked += (sender, args) => SetSelectedEmpty(MapLocationModel);
                    break;
                default:
                    

                    // Use the blank cell
                    data.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare.ImageURI;
                    break;
            }

            return data;
        }

        /// <summary>
        /// Set the Background color for the tile.
        /// Monsters and Characters have different colors
        /// Empty cells are transparent
        /// </summary>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public Color DetermineMapBackgroundColor(MapModelLocation MapModel)
        {
            string BattleMapBackgroundColor;
            switch (MapModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    BattleMapBackgroundColor = "BattleMapCharacterColor";
                    break;
                case PlayerTypeEnum.Monster:
                    BattleMapBackgroundColor = "BattleMapMonsterColor";
                    break;
                case PlayerTypeEnum.Unknown:
                default:
                    BattleMapBackgroundColor = "BattleMapTransparentColor";
                    break;
            }
  
            var result = (Color)Application.Current.Resources[BattleMapBackgroundColor];
            return result;
        }

        #region MapEvents
        /// <summary>
        /// Event when an empty location is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedEmpty(MapModelLocation data)
        {


            /*Move Character on Empty Space
             */

            BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn());

            var cell = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;
            

            if (cell.PlayerType != PlayerTypeEnum.Character)
            {
                return false;
            }


            var currentCellLocation = EngineSettings.MapModel.GetLocationForPlayer(cell);

            if (currentCellLocation == null)
            {
                return false;
            }

            //Message
            EngineSettings.BattleMessagesModel.TurnMessage = string.Format("{0} moves from {1},{2} to {3},{4}", cell.Name, currentCellLocation.Column, currentCellLocation.Row, data.Column, data.Row);
            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            //Move
            EngineSettings.MapModel.MovePlayerOnMap(currentCellLocation, data);
            GameMessage();

            ///DrawGameAttackerDefenderBoard();
            UpdateMapGrid();

            //BattleEngineViewModel.Instance.Engine.Round.EndRound()
            return true;

        }

        /// <summary>
        /// Event when a Monster is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedMonster(MapModelLocation data)
        {
            // TODO: Info

            /*
             * This gets called when the Monster is clicked on
             * Usefull if you want to select the monster to attack etc.
             * 
             * For Mike's simple battle grammar there is no selection of action so I just return true
             */

            //data.IsSelectedTarget = true;

            var cell = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();

            if (cell.PlayerType != PlayerTypeEnum.Character)
            {
                return false;

            }
            BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(cell);


            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Get the turn, set the current player and attacker to match
            //SetAttackerAndDefender();

            BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(data.Player);

            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;
            // Hold the current state
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            UpdateMapGrid();
            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();
            
            if (RoundCondition == RoundEnum.NewRound)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Pause
                Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                //ShowModalRoundOverPage();
                ShowBattleMode();
                RoundOver_Clicked(null, null);
                return true;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                //GameOver();
                //ShowBattleMode();
                GameOver_Clicked(null, null);
                return true;
            }
            return true;
        }

        /// <summary>
        /// Event when a Character is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedCharacter(MapModelLocation data)
        {
            // TODO: Info

            /*
             * This gets called when the characters is clicked on
             * Usefull if you want to select the character and then set state or do something
             * 
             * For Mike's simple battle grammar there is no selection of action so I just return true
             */

            return true;
        }
        #endregion MapEvents

        #endregion BattleMapMode

        #region BasicBattleMode

        /// <summary>
        /// Draw the UI for
        ///
        /// Attacker vs Defender Mode
        /// 
        /// </summary>
        public void DrawGameAttackerDefenderBoard()
        {
            // Clear the current UI
            DrawGameBoardClear();

            // Show Characters across the Top
            DrawPlayerBoxes();

            // Draw the Map
            UpdateMapGrid();

            // Show the Attacker and Defender
            DrawGameBoardAttackerDefenderSection();
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender
        /// </summary>
        public void DrawGameBoardAttackerDefenderSection()
        {
            BattlePlayerBoxVersus.Text = "";

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker == null)
            {
                return;
            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender == null)
            {
                return;
            }
            AttackerFrame.IsVisible = true; 
            AttackerImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.ImageURI;
            AttackerName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
            AttackerHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetMaxHealthTotal.ToString();

            DefenderFrame.IsVisible = true;
            DefenderImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.ImageURI;
            DefenderName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Name;
            DefenderHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetMaxHealthTotal.ToString();

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Alive == false)
            {
                UpdateMapGrid();
                DefenderImage.BackgroundColor = Color.FromHex("BE4647");
                DefenderFrame.BackgroundColor = Color.FromHex("BE4647");
                DefenderFrame.BorderColor = Color.FromHex("BE4647"); 

            }

            BattlePlayerBoxVersus.Text = "vs";
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender areas to be null
        /// </summary>
        public void DrawGameBoardClear()
        {
            AttackerImage.Source = string.Empty;
            AttackerName.Text = string.Empty;
            AttackerHealth.Text = string.Empty;

            DefenderImage.Source = string.Empty;
            DefenderName.Text = string.Empty;
            DefenderHealth.Text = string.Empty;
            DefenderImage.BackgroundColor = Color.Transparent;
            DefenderFrame.BackgroundColor = Color.White;
            DefenderFrame.BorderColor = Color.White;

            BattlePlayerBoxVersus.Text = string.Empty;
        }

        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackButton_Clicked(object sender, EventArgs e)
        {
            NextAttackExample();
        }

        /// <summary>
        /// Settings Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Setttings_Clicked(object sender, EventArgs e)
        {
            await ShowBattleSettingsPage();
        }

        /// <summary>
        /// Next Attack Example
        /// 
        /// This code example follows the rule of
        /// 
        /// Auto Select Attacker
        /// Auto Select Defender
        /// 
        /// Do the Attack and show the result
        /// 
        /// So the pattern is Click Next, Next, Next until game is over
        /// 
        /// </summary>
        public void NextAttackExample()
        {
            var cell = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();

            //if(cell.PlayerType == PlayerTypeEnum.Character)
            //{
            //    return;
            //}
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Get the turn, set the current player and attacker to match
            SetAttackerAndDefender();

            // Hold the current state
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();

            if (RoundCondition == RoundEnum.NewRound)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Pause
                Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();
                return;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver_Clicked(null, null);
                return;
            }
        }

        /// <summary>
        /// Decide The Turn and who to Attack
        /// </summary>
        public void SetAttackerAndDefender()
        {
            BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn());

            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // User would select who to attack

                    // for now just auto selecting
                    BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    break;

                case PlayerTypeEnum.Monster:
                default:

                    // Monsters turn, so auto pick a Character to Attack
                    BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    break;
            }
        }

        /// <summary>
        /// Game is over
        /// 
        /// Show Buttons
        /// 
        /// Clean up the Engine
        /// 
        /// Show the Score
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        public void GameOver()
        {
            // Save the Score to the Score View Model, by sending a message to it.
            var Score = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore;
            MessagingCenter.Send(this, "AddData", Score);

            ShowBattleMode();
        }
        #endregion BasicBattleMode

        #region MessageHandelers

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {
            // Output The Message that happened.

            BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage, BattleMessages.Text);

           
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Monster)
            {
                BattleMessages.TextColor = Color.FromHex("BE4647");
                BattleMessages.MaxLines = 1; 
            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
            {
                BattleMessages.TextColor = Color.FromHex("F6D55C");
                BattleMessages.MaxLines = 1;
            }

            Debug.WriteLine(BattleMessages.Text);

            if (!string.IsNullOrEmpty(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage, BattleMessages.Text);
                BattleMessages.MaxLines = 2;
            }


            //htmlSource.Html = BattleEngineViewModel.Instance.Engine.BattleMessagesModel.GetHTMLFormattedTurnMessage();
            //HtmlBox.Source = HtmlBox.Source = htmlSource;
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
            htmlSource.Html = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.GetHTMLBlankMessage();
            //HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers

        #region PageHandelers

        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        /// <summary>
        /// The Next Round Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;
            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// The Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        /// Show the Round Over page
        /// 
        /// Round Over is where characters get items
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        /// Round over button event on click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void RoundOver_Clicked(object sender, EventArgs args)
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        /// Game over button event on click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void GameOver_Clicked(object sender, EventArgs args)
        {
            GameUIDisplay.IsVisible = false;

            // Show the Game Over Display
            //GameOverDisplay.IsVisible = true;
           await Navigation.PushModalAsync(new RoundOverPage(true));
        }
        /// <summary>
        /// Show Settings
        /// </summary>
        public async Task ShowBattleSettingsPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new BattleSettingsPage());
        }
        #endregion PageHandelers

        /// <summary>
        /// Show the battle mode for different page contexts 
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ShowBattleMode();
        }

        /// <summary>
        /// 
        /// Hide the differnt button states
        /// 
        /// Hide the message display box
        /// 
        /// </summary>
        public void HideUIElements()
        {
            NextRoundButton.IsVisible = false;
            StartBattleButton.IsVisible = false;
            AttackButton.IsVisible = false;
            MessageDisplayBox.IsVisible = false;
            MessageDisplayFrame.IsVisible = false;
            BattlePlayerInfomationBox.IsVisible = false;
        }

        /// <summary>
        /// Show the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            // If running in UT mode, 
            if (UnitTestSetting)
            {
                return;
            }

            HideUIElements();

            ClearMessages();

            DrawPlayerBoxes();

            // Update the Mode
            BattleModeValue.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum.ToMessage();

            ShowBattleModeDisplay();

            ShowBattleModeUIElements();
        }

        /// <summary>
        /// Control the UI Elements to display
        /// </summary>
        public void ShowBattleModeUIElements()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum)
            {
                case BattleStateEnum.Starting:

                    StartBattleButton.IsVisible = true;
                    AttackButton.IsVisible = false;
                    UseAbilityButton.IsVisible = false;
                    UseItemButton.IsVisible = false;
                    MapGrid.IsEnabled = false;
                    break;

                case BattleStateEnum.NewRound:
                    UpdateMapGrid();
                    NextRoundButton.IsVisible = true;
                    AttackButton.IsVisible = false;
                    UseAbilityButton.IsVisible = false;
                    UseItemButton.IsVisible = false;
                    MapGrid.IsEnabled = false;
                    break;

                case BattleStateEnum.GameOver:
                    // Hide the Game Board
                    GameUIDisplay.IsVisible = false;

                    // Show the Game Over Display
                    //GameOverDisplay.IsVisible = true;
                    break;

                case BattleStateEnum.RoundOver:
                case BattleStateEnum.Battling:
                    GameUIDisplay.IsVisible = true;
                    BattlePlayerInfomationBox.IsVisible = true;
                    MessageDisplayBox.IsVisible = true;
                    MessageDisplayFrame.IsVisible = true;
                    AttackButton.IsVisible = true;
                    MapGrid.IsEnabled = true;
                    break;

                // Based on the State disable buttons
                case BattleStateEnum.Unknown:
                default:
                    break;
            }
        }

        /// <summary>
        /// Control the Map Mode or Simple
        /// </summary>
        public void ShowBattleModeDisplay()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum)
            {
                case BattleModeEnum.MapAbility:
                case BattleModeEnum.MapFull:
                    GamePlayersTopDisplay.IsVisible = false;
                    BattleMapDisplay.IsVisible = true;
                    UseAbilityButton.IsVisible = true;
                    UseItemButton.IsVisible = true;
                    break;

                case BattleModeEnum.MapNext:
                    GamePlayersTopDisplay.IsVisible = false;
                    BattleMapDisplay.IsVisible = true;
                    UseAbilityButton.IsVisible = false;
                    UseItemButton.IsVisible = false;
                    break;

                case BattleModeEnum.SimpleAbility:
                case BattleModeEnum.SimpleNext:
                case BattleModeEnum.Unknown:
                default:
                    GamePlayersTopDisplay.IsVisible = true;
                    BattleMapDisplay.IsVisible = false;
                    UseAbilityButton.IsVisible = false;
                    UseItemButton.IsVisible = false;
                    break;
            }
        }

        /// <summary>
        /// Displays emergency kit list
        /// </summary>
        public void AddItemToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();

            foreach (var data in FlexList)
            {
                ItemBox.Children.Remove(data);
            }

            // List of consumable
            var EmergencyKit = EngineSettingsModel.Instance.EmergencyKit.ToList();

            foreach (var consumable in EmergencyKit)
            {
                ItemBox.Children.Add(GetItemToDisplay(consumable));
            }

        }

        /// <summary>
        /// Look up items to display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemModel consumable)
        {

            // Default clickable button
            var ClickAbleButton = true;

            // If item is not equipped, will not render anything
            if (consumable == null)
            {

                return new StackLayout { };

            }

            // Item image as ImageButton for pop-up detail view
            var ItemButton = new ImageButton
            {
                Source = consumable.ImageURI,
                WidthRequest = 40,
                HeightRequest = 40,
                BackgroundColor = Color.White,
                CornerRadius = 40,
                Padding = 5,
            };

            // Clicked to unequip item from player
            if (ClickAbleButton)
            {
                ItemButton.Clicked += (sender, args) => UseConsumableItem(consumable);
            }

            // Label for each item
            var ItemLabel = new Label
            {
                Text = consumable.Name,
                HorizontalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.HeadTruncation

            };

            // Frame for label, so that the text will wrap if text is long
            var ItemFrame = new Frame
            {
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent,
                WidthRequest = 100,

                Content = ItemLabel,
            };

            // Put ImageButton and label in to same stack
            var ItemStack = new StackLayout
            {
                Padding = 3,
                HorizontalOptions = LayoutOptions.Center,

                Children = { ItemButton, ItemFrame},
            };

            // Render particuler stack
            return ItemStack;
        }

        /// <summary>
        /// Event handler when click use item botton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UseItemButton_Clicked(object sender, EventArgs e)
        {
            // Display player info popup
            CharacterPopUpFrame.IsVisible = true;

            GetPlayerInfo();

            AddItemToDisplay();
        }

        /// <summary>
        /// Seed the correct info to popup
        /// </summary>
        public void GetPlayerInfo()
        {
            // Current turn player
            var cell = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();

            // Player status
            String status = "Happy";

            if (cell.Poison)
            {
                status = "Poisoned";
            }

            CharacterPopUpName.Text = cell.Name;
            CharacterPopUpImage.Source = cell.ImageURI;
            CharacterPopupCellType.Text = cell.Job.ToString();
            CharacterPopupHealth.Text = cell.GetCurrentHealth() + " / " + cell.GetMaxHealth();
            CharacterPopupAttack.Text = cell.GetAttack().ToString();
            CharacterPopupDefense.Text = cell.GetDefense().ToString();
            CharacterPopupSpeed.Text = cell.GetSpeed().ToString();
            CharacterPopupStatus.Text = status;
        }

        /// <summary>
        ///  Close popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            CharacterPopUpFrame.IsVisible = false;
        }

        /// <summary>
        /// Use consumable in emergency kit
        /// </summary>
        /// <param name="consumable"></param>
        /// <returns></returns>
        public bool UseConsumableItem(ItemModel consumable)
        {
            // Default result
            var result = false;

            // Current turn player
            var cell = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();

            switch (consumable.Name)
            {
                case "Antidote":
                    result = UseAntidote(cell);
                    break;

                case "Adrenaline Syringe":

                    result = UseAdrenaline(cell);
                    break;

                case "Band aid":

                    result = UseBandAid(cell);
                    break;

                case "Gummy Multi-Vitamin":

                    result = UseGummy(cell);
                    break;
            };

            if (result)
            {
                // Remove consumable out of emergency kit after used
                EngineSettingsModel.Instance.EmergencyKit.Remove(consumable);
            }

            // Refresh player data
            GetPlayerInfo();

            // Refresh emergency list
            AddItemToDisplay();


            return result;
        }

        /// <summary>
        /// Remove poison from player
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool UseAntidote(PlayerInfoModel cell)
        { 
            // Cure poison
            cell.Poison = false;

            return true;

        }

        /// <summary>
        /// Increase speed for round
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool UseAdrenaline(PlayerInfoModel cell)
        {
            cell.BuffSpeed();

            return true;
        }

        /// <summary>
        /// Restore small amount of health
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool UseBandAid(PlayerInfoModel cell)
        {
            cell.CurrentHealth += 10;

            // If current exceed max health, current health = max health
            if (cell.CurrentHealth > cell.MaxHealth)
            {
                cell.CurrentHealth = cell.MaxHealth;
            }

            return true;

        }

        /// <summary>
        /// Increase defense for round
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool UseGummy(PlayerInfoModel cell)
        {

            cell.BuffDefense();

            return true;
        }
    }
}