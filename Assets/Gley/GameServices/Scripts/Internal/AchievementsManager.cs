﻿namespace Gley.GameServices.Internal
{
    using UnityEngine;
    using System.Collections.Generic;
    using UnityEngine.Events;

#if GLEY_GAMESERVICES_IOS
    using UnityEngine.SocialPlatforms.GameCenter;
#endif

#if GLEY_GAMESERVICES_ANDROID
    using GooglePlayGames;
#endif

    public class AchievementsManager
    {
        //load the list of all game achievements form the Settings Window
        public List<Achievement> gameAchievements;

        /// <summary>
        /// Constructor, loads settings data
        /// </summary>
        public AchievementsManager()
        {
            try
            {
                gameAchievements = Resources.Load<GameServicesData>(Constants.DATA_NAME_RUNTIME).allGameAchievements;
            }
            catch
            {
                Debug.LogError("Game Services Data not found -> Go to Tools->Gley->Game Services to setup the plugin");
            }
        }

        /// <summary>
        /// Submit an achievements for both ANdroid and iOS using the Unity Social interface
        /// </summary>
        /// <param name="name">Achievement name</param>
        /// <param name="SubmitComplete">callback triggered when an achievement submit is complete</param>
        public void SumbitAchievement(AchievementNames name, UnityAction<bool, GameServicesError> SubmitComplete)
        {
            for (int i = 0; i < gameAchievements.Count; i++)
            {
                if (gameAchievements[i].name == name.ToString())
                {
                    if (gameAchievements[i].achivementComplete == false)
                    {
#if GLEY_GAMESERVICES_IOS
                        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
#endif
                        gameAchievements[i].achivementComplete = true;
                        string id = "";
#if GLEY_GAMESERVICES_ANDROID
                        id = gameAchievements[i].idGoogle;
#endif
#if GLEY_GAMESERVICES_IOS
                        id = gameAchievements[i].idIos;
#endif
                        Social.ReportProgress(id, 100.0f, (bool success) =>
                        {
                            if (success)
                            {
                                if (SubmitComplete != null)
                                {
                                    SubmitComplete(true, GameServicesError.Success);
                                }

                            }
                            else
                            {
                                if (SubmitComplete != null)
                                {
                                    SubmitComplete(false, GameServicesError.AchievementSubmitFailed);
                                }
                            }
                        });
                    }
                    else
                    {
                        if (SubmitComplete != null)
                        {
                            SubmitComplete(true, GameServicesError.AchievementAlreadySubmitted);
                        }
                    }
                }
            }
        }

        public bool IsComplete(AchievementNames name)
        {
            for (int i = 0; i < gameAchievements.Count; i++)
            {
                if (gameAchievements[i].name == name.ToString())
                {
                    return gameAchievements[i].achivementComplete;
                }
            }
            return false;
        }

        /// <summary>
        /// Not use yet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SubmitEvent(AchievementNames name, uint value)
        {
#if GLEY_GAMESERVICES_ANDROID
            for (int i = 0; i < gameAchievements.Count; i++)
            {
                if (gameAchievements[i].name == name.ToString())
                {
                    PlayGamesPlatform.Instance.Events.IncrementEvent(gameAchievements[i].idGoogle, value);
                }
            }
#endif
        }


        public void IncrementAchievement(AchievementNames name, int steps, UnityAction<bool, GameServicesError> SubmitComplete)
        {
#if GLEY_GAMESERVICES_ANDROID
            for (int i = 0; i < gameAchievements.Count; i++)
            {
                if (gameAchievements[i].name == name.ToString())
                {
                    PlayGamesPlatform.Instance.IncrementAchievement(gameAchievements[i].idGoogle, steps, (bool success)=>
                    {
                        if(SubmitComplete!=null)
                        {
                            if (success)
                            {
                                SubmitComplete(success, GameServicesError.Success);
                            }
                            else
                            {
                                SubmitComplete(success, GameServicesError.AchievementSubmitFailed);
                            }
                        }
                    });
                }
            }
#endif
#if GLEY_GAMESERVICES_IOS
            for (int i = 0; i < gameAchievements.Count; i++)
            {
                if (gameAchievements[i].name == name.ToString())
                {
                    Social.ReportProgress(gameAchievements[i].idIos, steps, (bool success) =>
                    {
                        if (SubmitComplete != null)
                        {
                            if (success)
                            {
                                if (SubmitComplete != null)
                                {
                                    SubmitComplete(success, GameServicesError.Success);
                                }
                            }
                            else
                            {
                                if (SubmitComplete != null)
                                {
                                    SubmitComplete(success, GameServicesError.AchievementSubmitFailed);
                                }
                            }
                        }
                    });
                }
            }
#endif
        }

        /// <summary>
        /// Show status of all achievements in the game
        /// </summary>
        public void ShowAchievements()
        {
            Social.ShowAchievementsUI();
        }
    }
}
