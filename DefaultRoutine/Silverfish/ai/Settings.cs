using System;
using System.IO;

namespace HREngine.Bots
{
    public class Settings
    {
        // play with these settings###################################
        public int enfacehp = 15;  // hp of enemy when your hero is allowed to attack the enemy face with his weapon without penalty
        // weaponOnlyAttackMobsUntilEnfacehp - If your opponent has more HP than enfacehp, then weapons are allowed only attack mobs
        // 0 - don't attack face until enfacehp (except weapons with 1 Attack)
        // 1 - don't attack face until enfacehp if weapon's durability = 1 (if durability > 1 then it's allowed)(except weapons with 1 Attack)
        // 2 - don't attack face until enfacehp (any weapon)
        // 3 - don't attack face until enfacehp if weapon's durability = 1 (if durability > 1 then it's allowed)(any weapon)
        // 4 - don't attack face until enfacehp (except: you have any* weapon generating card in hand)(* except Upgrade!)
        // 5 - don't attack face until enfacehp (except: you have any* weapon generating card in hand with attack > 1 (or if they both have attack = 1))(* except Upgrade!)
        public int weaponOnlyAttackMobsUntilEnfacehp = 5;
        public int maxwide = 3000;   // numer of boards which are taken to the next deep-lvl

        public bool playaround = false;  //play around some enemys aoe-spells
        // these two parameters are value between 0 and 100 (0 <= Your_Value <= 100)
        public int playaroundprob = 50;    // probability where the enemy NOT plays the aoe-spell: 100 - enemy never plays aoe-spell, 0 - always uses
        public int playaroundprob2 = 80;   // probability where the enemy plays the aoe-spell, and your minions will survive: 100 - always survive, 0 - never(survival depends on their real HP)

        public int twotsamount = 0; // number of boards where the second AI step is simulated
        public int enemyTurnMaxWide = 40; // // max number of enemy boards calculated in enemys-first-turn first AI step (lower than enemyTurnMaxWideSecondStep)
        public int enemyTurnMaxWideSecondStep = 200; // max number of enemy boards calculated in enemys-first-turn second AI step(higher than enemyTurnMaxWide)

        public int nextTurnDeep = 6; //maximum combo-deep in your second turn (dont change this!)
        public int nextTurnMaxWide = 20; //maximum best boards for calculation at each step in the second round
        public int nextTurnTotalBoards = 200; //maximum boards calculated in second turn simulation
        public int berserkIfCanFinishNextTour = 0; // 0 - off, 1 - if there is any chance to kill the enemy through the round, all attacks will be in the face

        public int alpha = 50; // weight of the second turn in calculation (0<= alpha <= 100)
        public bool useSecretsPlayAround = true; // playing arround enemys secrets
        public int placement = 0;  // 0 - minions are interleaved by value (..low value - hi value..), 1 - hi val minions along the edges, low val in the center

        //use this only with useExternalProcess = true !!!!
        public bool useExternalProcess = false; // // use external process for calculations
        public bool passiveWaiting = false; // process will wait passive for external process to finish calculations

        public int speedupLevel = 0; // 0 - normal speed (the safest - wait animation), 1 - speed up face attacks, 2 - 1 + speed up attack on minions
        public int adjustActions = 0; // test!! - reorder actions after calculations: 0 - as calculated (by Default), 1 - AoE first
        public int printRules = 0; //0 - off, 1 - on
        public int concedeMode = 0; //0 - concede if direct defeat, 1 - concede if Playfield has a value <= -10000
        //###########################################################

        public float firstweight = 0.5f;
        public float secondweight = 0.5f;
        public int numberOfThreads = 32;
        public bool simulateEnemysTurn = true;
        public int secondTurnAmount = 256;
        public string mainPath = "";
        public string path = "";
        public string logpath = "";
        public string logfile = "Logg.txt";
        public bool concede = false;
        public bool enemyConcede = false;
        public bool writeToSingleFile = false;
        public bool learnmode = true;
        public bool printlearnmode = true;
        public bool test = false;

        private static Settings instance;

        public static Settings Instance
        {
            get
            {
                return instance ?? (instance = new Settings());
            }
        }

        private Settings()
        {
            this.writeToSingleFile = false;
        }

        public void setSettings(string behavName, bool nameIsPath = false)
        {
            if (test) return;
            string pathToSettings = behavName;
            if (test)
            {
                pathToSettings = mainPath + "behavior\\" + behavName + "\\_settings.txt";
            }
            else if (!nameIsPath)
            {
                if (!Silverfish.Instance.BehaviorPath.ContainsKey(behavName))
                {
                    Helpfunctions.Instance.ErrorLog(behavName + ": 本策略没有配置文件.");
                    endOfSetSettings();
                    return;
                }
                pathToSettings = Path.Combine(Silverfish.Instance.BehaviorPath[behavName], "_settings_custom.txt");
                if (System.IO.File.Exists(pathToSettings)) Helpfunctions.Instance.ErrorLog(behavName + ": 应用自定义配置.");
                else pathToSettings = Path.Combine(Silverfish.Instance.BehaviorPath[behavName], "_settings.txt");
            }

            if (!System.IO.File.Exists(pathToSettings))
            {
                Helpfunctions.Instance.ErrorLog(behavName + ": 没有设置.");
                endOfSetSettings();
                return;
            }
            try
            {
                Helpfunctions.Instance.ErrorLog("读取战场设置 " + behavName);
                string[] lines = System.IO.File.ReadAllLines(pathToSettings);
                String[] tmp;
                int valueInt;
                bool valueBool = false;
                foreach (string s in lines)
                {
                    if (s == "" || s == null) continue;
                    if (s.StartsWith("//")) continue;
                    tmp = s.Split(';')[0].Split(' ');
                    if (tmp.Length != 3) continue;

                    if (!Int32.TryParse(tmp[2], out valueInt))
                    {
                        switch (tmp[2])
                        {
                            case "true": valueBool = true; break;
                            case "false": valueBool = false; break;
                        }
                    }

                    switch (tmp[0])
                    {
                        case "enfacehp": this.enfacehp = valueInt; break;
                        case "weaponOnlyAttackMobsUntilEnfacehp": this.weaponOnlyAttackMobsUntilEnfacehp = valueInt; break;
                        // case "maxwide": this.maxwide = valueInt; break;
                        case "playaround": this.playaround = valueBool; break;
                        case "playaroundprob": this.playaroundprob = valueInt; break;
                        case "playaroundprob2": this.playaroundprob2 = valueInt; break;
                        case "twotsamount": this.twotsamount = valueInt; break;
                        case "enemyTurnMaxWide": this.enemyTurnMaxWide = valueInt; break;
                        case "enemyTurnMaxWideSecondStep": this.enemyTurnMaxWideSecondStep = valueInt; break;
                        case "berserkIfCanFinishNextTour": this.berserkIfCanFinishNextTour = valueInt; break;
                        case "concedeMode": this.concedeMode = valueInt; break;                            
                        case "printRules": this.printRules = valueInt; break;
                        case "nextTurnDeep": this.nextTurnDeep = valueInt; break;
                        case "nextTurnMaxWide": this.nextTurnMaxWide = valueInt; break;
                        case "nextTurnTotalBoards": this.nextTurnTotalBoards = valueInt; break;
                        case "useSecretsPlayAround": this.useSecretsPlayAround = valueBool; break;
                        case "alpha": this.alpha = valueInt; break;
                        case "placement": this.placement = valueInt; break;
                        case "useExternalProcess": this.useExternalProcess = valueBool; break;
                        case "passiveWaiting": this.passiveWaiting = valueBool; break;
                        case "speedupLevel": this.speedupLevel = valueInt; break;
                        case "adjustActions": this.adjustActions = valueInt; break;
                        default:
                            Helpfunctions.Instance.ErrorLog(tmp[2] + " is not supported!!!");
                            break;
                    }
                }
                endOfSetSettings();
            }
            catch (Exception e)
            {
                Helpfunctions.Instance.ErrorLog(behavName + " _settings.txt - 读取错误. 我们将使用默认配置. 异常: " + e.Message);
                endOfSetSettings();
                return;
            }
            Helpfunctions.Instance.ErrorLog(behavName + " 配置文件读取成功.");
            printSettings();
        }

        private void endOfSetSettings()
        {
            setWeights(this.alpha);

            Helpfunctions.Instance.ErrorLog("设置怼脸血线值为: " + this.enfacehp);
            Helpfunctions.Instance.ErrorLog("设置武器怼脸血线值: " + this.weaponOnlyAttackMobsUntilEnfacehp);
            ComboBreaker.Instance.attackFaceHP = this.enfacehp;

            Ai.Instance.setMaxWide(DefaultRoutineSettings.Instance.MaxWide);
            Helpfunctions.Instance.ErrorLog("设置AI值（maxwide）: " + DefaultRoutineSettings.Instance.MaxWide);
            Ai.Instance.setTwoTurnSimulation(false, this.twotsamount);
            Helpfunctions.Instance.ErrorLog("计算下个回合 " + this.twotsamount + " 线程");
            if (this.twotsamount > 0) Helpfunctions.Instance.ErrorLog("推算下回合敌方行动");
            if (this.useSecretsPlayAround) Helpfunctions.Instance.ErrorLog("开启防奥秘");
            if (this.playaround)
            {
                Ai.Instance.setPlayAround();
                Helpfunctions.Instance.ErrorLog("开启防AOE");
            }
            if (this.writeToSingleFile) Helpfunctions.Instance.ErrorLog("write log to single file");

        }

        public void setWeights(int alpha)
        {
            float a = ((float)alpha) / 100f;
            this.firstweight = 1f - a;
            this.secondweight = a;
            Helpfunctions.Instance.ErrorLog("目前的AI值（alpha）是 " + this.secondweight);
        }

        public void setFilePath(string path)
        {
            this.path = path;
        }
        public void setLoggPath(string path)
        {
            this.logpath = path;
        }

        public void setLoggFile(string path)
        {
            this.logfile = path;
        }

        public void printSettings()
        {
            Helpfunctions.Instance.logg("#################### Settings #########################################");
            Helpfunctions.Instance.logg("path = " + Settings.Instance.path);
            Helpfunctions.Instance.logg("logpath = " + Settings.Instance.logpath);
            Helpfunctions.Instance.logg("logfile = " + Settings.Instance.logfile);
            Helpfunctions.Instance.logg("twotsamount = " + Settings.Instance.twotsamount);
            Helpfunctions.Instance.logg("secondTurnAmount = " + Settings.Instance.secondTurnAmount);
            Helpfunctions.Instance.logg("playaroundprob2 = " + Settings.Instance.playaroundprob2);
            Helpfunctions.Instance.logg("playaroundprob = " + Settings.Instance.playaroundprob);
            Helpfunctions.Instance.logg("nextTurnTotalBoards = " + Settings.Instance.nextTurnTotalBoards);
            Helpfunctions.Instance.logg("nextTurnMaxWide = " + Settings.Instance.nextTurnMaxWide);
            Helpfunctions.Instance.logg("nextTurnDeep = " + Settings.Instance.nextTurnDeep);
            Helpfunctions.Instance.logg("maxwide = " + Settings.Instance.maxwide);
            Helpfunctions.Instance.logg("enfacehp = " + Settings.Instance.enfacehp);
            Helpfunctions.Instance.logg("weaponOnlyAttackMobsUntilEnfacehp = " + Settings.Instance.weaponOnlyAttackMobsUntilEnfacehp);
            Helpfunctions.Instance.logg("enemyTurnMaxWideSecondStep = " + Settings.Instance.enemyTurnMaxWideSecondStep);
            Helpfunctions.Instance.logg("enemyTurnMaxWide = " + Settings.Instance.enemyTurnMaxWide);
            Helpfunctions.Instance.logg("alpha = " + Settings.Instance.alpha);
            Helpfunctions.Instance.logg("secondweight = " + Settings.Instance.secondweight);
            Helpfunctions.Instance.logg("firstweight = " + Settings.Instance.firstweight);
            Helpfunctions.Instance.logg("writeToSingleFile = " + Settings.Instance.writeToSingleFile);
            Helpfunctions.Instance.logg("useSecretsPlayAround = " + Settings.Instance.useSecretsPlayAround);
            Helpfunctions.Instance.logg("useExternalProcess = " + Settings.Instance.useExternalProcess);
            Helpfunctions.Instance.logg("placement = " + Settings.Instance.placement);
            Helpfunctions.Instance.logg("simulateEnemysTurn = " + Settings.Instance.simulateEnemysTurn);
            Helpfunctions.Instance.logg("printlearnmode = " + Settings.Instance.printlearnmode);
            Helpfunctions.Instance.logg("playaround = " + Settings.Instance.playaround);
            Helpfunctions.Instance.logg("passiveWaiting = " + Settings.Instance.passiveWaiting);
            Helpfunctions.Instance.logg("learnmode = " + Settings.Instance.learnmode);
            Helpfunctions.Instance.logg("enemyConcede = " + Settings.Instance.enemyConcede);
            Helpfunctions.Instance.logg("concede = " + Settings.Instance.concede);
            Helpfunctions.Instance.logg("speedupLevel = " + Settings.Instance.speedupLevel);
            Helpfunctions.Instance.logg("adjustActions = " + Settings.Instance.adjustActions);
            Helpfunctions.Instance.logg("#################### Settings End #####################################");
        }
    }
}