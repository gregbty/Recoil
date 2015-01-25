using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Recoil_2;

namespace Recoil.Shared
{
    public class GameEngine : Game
    {
        private const float playerAttackRate = 1000;
        private const float bulletCollisionDistance = 25;
        private const float enemyAttackCollisionDistance = 10;
        private const float itemCollisionDistance = 5;
        private readonly string HighScoresFilename = "highscores.lst";
        private readonly GraphicsDeviceManager graphics;
        private Texture2D background;
        private Texture2D bulletTexture;
        private List<Bullet> bullets;
        private Texture2D cannonBox;
        private GameScreen currentGameState;
        private KeyboardState currentKeyboard;
        private int enemiesKilled;
        private List<Enemy> enemy;
        private float fireTime;
        private float fireTime2;
        private SpriteFont font;
        private bool gameIsOver;
        private bool gameStartUp;
        private string highScoreMsg;
        private Texture2D highscore;
        private Texture2D instructions;
        private bool isAttacking;
        private bool isAttacking2;
        private bool isExit;
        private bool isFacingLeft;
        private bool isFacingLeft2;
        private bool isHS;
        private bool isInstructions;
        private bool isIntiails;
        private bool isMP;
        private bool isMainMenu;
        private bool isRunning;
        private bool isRunning2;
        private bool isSP;
        private int itemMaxCount;
        private int level;
        private int maxEnemyCount;
        private string name;
        private Player p;
        private Player p2;

        //Weapons
        private Texture2D pCannonTexture;
        private Vector2 pLocation;
        private Vector2 pLocation2;
        private Texture2D pPistolTexture;
        private Texture2D pSplicerTexture;
        private Texture2D pTexture;
        private double playerAttackTime;
        private double playerAttackTime2;
        private int playerHP;
        private int playerHP2;
        private KeyboardState previousKeyboard;
        private Random r;
        private int score;

        //Bullets
        private Texture2D slothTexture;
        private Texture2D splicerBox;
        private SpriteBatch spriteBatch;
        private Texture2D starkTexture;
        private Vector2 startPosition;
        private double timeSinceLastAnimation;
        private int timeSinceLastSpawn;
        private double timeSinceLastUpdate;
        private Texture2D title;

        //GameStates
        private Menu titleMenu;
        private int totalEnemiesKilled;
        private int totalEnemyCount;
        private int totalItemCount;
        private List<Weapon> weaponBox;
        private Texture2D zombieTexture;

        public GameEngine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            currentGameState = GameScreen.title;
            base.Initialize();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            r = new Random();

            pTexture = Content.Load<Texture2D>("player");
            pPistolTexture = Content.Load<Texture2D>("player_pistol");
            pCannonTexture = Content.Load<Texture2D>("player_cannon");
            pSplicerTexture = Content.Load<Texture2D>("player_splicer");
            bulletTexture = Content.Load<Texture2D>("bullet");
            zombieTexture = Content.Load<Texture2D>("zombie");
            starkTexture = Content.Load<Texture2D>("stark");
            slothTexture = Content.Load<Texture2D>("sloth");
            background = Content.Load<Texture2D>("background");
            cannonBox = Content.Load<Texture2D>("cannon_box");
            splicerBox = Content.Load<Texture2D>("splicer_box");
            title = Content.Load<Texture2D>("titleBackground");
            instructions = Content.Load<Texture2D>("instructionsBackground");
            highscore = Content.Load<Texture2D>("highScoreBackground");

            font = Content.Load<SpriteFont>("arial");
            titleMenu = new Menu(Color.White, Color.Red, font);
            titleMenu.addMenuItem("Singleplayer", new Vector2(70, 300));
            titleMenu.addMenuItem("Multiplayer", new Vector2(70, 360));
            titleMenu.addMenuItem("Instructions", new Vector2(70, 420));
            titleMenu.addMenuItem("High Scores", new Vector2(70, 480));
            titleMenu.addMenuItem("Exit", new Vector2(70, 540));

            isInstructions = false;
            isSP = false;
            isMP = false;
            isHS = false;
            isIntiails = false;
            isExit = false;
            isMainMenu = false;
            gameIsOver = false;
            gameStartUp = true;

            name = null;
            highScoreMsg = "Enter your initials here: ";
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here
            currentKeyboard = Keyboard.GetState();

            SetGameScreens(gameTime);

            if (currentGameState == GameScreen.singlePlayer || currentGameState == GameScreen.multiPlayer)
            {
                foreach (Enemy e in enemy)
                {
                    e.timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
                    e.timeSinceLastAttack += gameTime.ElapsedGameTime.TotalMilliseconds;
                }

                playerAttackTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
                playerAttackTime2 -= gameTime.ElapsedGameTime.TotalMilliseconds;
                timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
                timeSinceLastAnimation += gameTime.ElapsedGameTime.TotalMilliseconds;
                fireTime -= (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                fireTime2 -= (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                timeSinceLastSpawn += gameTime.TotalGameTime.Seconds;

                PlayerAttackCollision();
                EnemyAttackCollision();
                getUserInput(gameTime);
                SpawnEnemy();
                SpawnItem();
                ChangeWeapon();
                DropWeapon();
                MoveBullets();

                foreach (Enemy e in enemy)
                {
                    if (Vector2.Distance(e.Position, pLocation) < 35)
                    {
                        e.isAttacking = true;
                        e.isMoving = false;
                        e.Attack();
                    }

                    else
                    {
                        e.isMoving = true;
                        e.isAttacking = false;
                        e.Move();
                    }

                    if (isMP)
                    {
                        if (Vector2.Distance(e.Position, pLocation2) < 35)
                        {
                            e.isAttacking = true;
                            e.isMoving = false;
                            e.Attack();
                        }

                        else
                        {
                            e.isMoving = true;
                            e.isAttacking = false;
                            e.Move();
                        }
                    }

                    if (timeSinceLastAnimation > 150)
                    {
                        timeSinceLastAnimation = 0;

                        if (isRunning == false)
                        {
                            p.AnimateHead();
                        }

                        if (isRunning2 == false)
                        {
                            p2.AnimateHead();
                        }
                    }
                }
            }
            previousKeyboard = currentKeyboard;

            if (gameIsOver)
            {
                SaveHighScore();
                isMainMenu = true;
            }

            base.Update(gameTime);
        }

        private void ResetGame()
        {
            p = new Player(pTexture, pLocation, playerHP);
            pLocation = new Vector2(500, graphics.PreferredBackBufferHeight - 200);
            playerHP = 500;

            p2 = new Player(pTexture, pLocation2, playerHP2);
            pLocation2 = new Vector2(540, graphics.PreferredBackBufferHeight - 200);
            playerHP2 = 500;

            p.AddWeapon(new Pistol(5, 1, pPistolTexture, pLocation));
            p2.AddWeapon(new Pistol(5, 1, pPistolTexture, pLocation2));

            playerAttackTime = 0;
            playerAttackTime2 = 0;
            isAttacking = false;
            isAttacking2 = false;

            isFacingLeft = false;
            isRunning = false;
            startPosition = new Vector2(0, 0);
            timeSinceLastUpdate = 0;
            timeSinceLastAnimation = 0;

            bullets = new List<Bullet>();
            fireTime = 0;
            fireTime2 = 0;

            totalEnemyCount = 0;
            maxEnemyCount = 5;
            timeSinceLastSpawn = 0;

            level = 1;
            score = 0;
            enemiesKilled = 0;
            totalEnemiesKilled = 0;

            enemy = new List<Enemy>();
            weaponBox = new List<Weapon>();
            gameStartUp = false;
        }

        private void SaveHighScore()
        {
            HighScoreData data = LoadHighScores(HighScoresFilename);

            int scoreIndex = -1;
            for (int i = 0; i < data.Count; i++)
            {
                if (score > data.highScore[i])
                {
                    scoreIndex = i;
                    break;
                }
            }

            if (scoreIndex > -1)
            {
                for (int i = data.Count - 1; i > scoreIndex; i--)
                {
                    data.playerName[i] = data.playerName[i - 1];
                    data.highScore[i] = data.highScore[i - 1];
                }

                data.playerName[scoreIndex] = "Player"; //Retrieve User Name Here
                data.highScore[scoreIndex] = score;

                SaveHighScores(data, HighScoresFilename);
            }
        }

        private string getUserName()
        {
            KeyboardState previousKeystrokes;
            KeyboardState currentKeystrokes;
            currentKeystrokes = Keyboard.GetState();
            previousKeystrokes = currentKeystrokes;

            string playerName;
            playerName = null;

            Keys[] pressedKeys;
            pressedKeys = currentKeystrokes.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (previousKeystrokes.IsKeyUp(key))
                {
                    if (key == Keys.Back)
                    {
                        playerName = playerName.Remove(playerName.Length - 1, 1);
                    }

                    else if (key == Keys.Space)
                    {
                        playerName = playerName.Insert(playerName.Length, "");
                    }

                    else
                        playerName += key.ToString();
                }

                if (currentKeyboard.IsKeyDown(Keys.Enter))
                {
                    return playerName;
                }
            }
            return playerName;
        }

        public static void SaveHighScores(HighScoreData data, string filename)
        {
            using (Stream stream = TitleContainer.OpenStream(filename))
            {
                var serializer = new XmlSerializer(typeof (HighScoreData));
                serializer.Serialize(stream, data);
            }
        }

        public static HighScoreData LoadHighScores(string filename)
        {
            HighScoreData data;

            using (Stream stream = TitleContainer.OpenStream(filename))
            {
                var serializer = new XmlSerializer(typeof (HighScoreData));
                data = (HighScoreData) serializer.Deserialize(stream);
            }

            return (data);
        }

        private void SetGameScreens(GameTime gameTime)
        {
            isMainMenu = true;
            if (currentGameState.Equals(GameScreen.title))
            {
                isMainMenu = true;
                gameStartUp = true;

                if (gameStartUp)
                {
                    ResetGame();
                }

                if (isInstructions)
                {
                    currentGameState = GameScreen.instructions;
                    return;
                }
                if (isExit)
                {
                    Exit();
                }

                if (isSP)
                {
                    currentGameState = GameScreen.singlePlayer;
                    return;
                }

                if (isMP)
                {
                    currentGameState = GameScreen.multiPlayer;
                    return;
                }

                if (isHS)
                {
                    currentGameState = GameScreen.highScores;
                }

                titleMenu.getUserInput(gameTime);
                if (currentKeyboard.IsKeyDown(Keys.Enter))
                {
                    int sNum = titleMenu.getSelectedNum();

                    switch (sNum)
                    {
                        case 0:
                            isSP = true;
                            break;
                        case 1:
                            isMP = true;
                            break;
                        case 2:
                            isInstructions = true;
                            break;
                        case 3:
                            isHS = true;
                            break;
                        case 4:
                            isExit = true;
                            break;
                    }
                }
            }

            if (currentGameState == GameScreen.singlePlayer)
            {
                isMainMenu = false;
                if (currentKeyboard.IsKeyDown(Keys.Escape) || gameIsOver)
                {
                    isSP = false;
                    currentGameState = GameScreen.title;
                    return;
                }
            }

            if (currentGameState == GameScreen.multiPlayer)
            {
                isMainMenu = false;
                if (currentKeyboard.IsKeyDown(Keys.Escape) || gameIsOver)
                {
                    isMP = false;
                    currentGameState = GameScreen.title;
                    return;
                }

                if (isMainMenu)
                {
                    isMP = false;
                    name = getUserName();
                    currentGameState = GameScreen.title;
                    return;
                }
            }

            if (currentGameState == GameScreen.instructions)
            {
                isMainMenu = false;
                if (currentKeyboard.IsKeyDown(Keys.Escape))
                {
                    isInstructions = false;
                    currentGameState = GameScreen.title;
                    return;
                }
            }

            if (currentGameState == GameScreen.highScores)
            {
                isMainMenu = false;
                if (currentKeyboard.IsKeyDown(Keys.Escape))
                {
                    isHS = false;
                    currentGameState = GameScreen.title;
                }
            }
        }

        private void PlayerAttackCollision()
        {
            var enemyCenter = new Vector2();
            var bulletCenter = new Vector2();
            for (int i = enemy.Count - 1; i >= 0; i--)
            {
                enemyCenter.X = enemy[i].position.X + 30;
                enemyCenter.Y = enemy[i].position.Y + 75;
                for (int j = bullets.Count - 1; j >= 0; j--)
                {
                    bulletCenter.X = bullets[j].position.X + 5;
                    bulletCenter.Y = bullets[j].position.Y + 5;
                    if (Vector2.Distance(bulletCenter, enemyCenter) <= bulletCollisionDistance)
                    {
                        bullets.RemoveAt(j);
                        if (enemy[i].health_points == 0)
                        {
                            if (enemy[i].id == "zombie")
                            {
                                score += 10;
                            }
                            else if (enemy[i].id == "stark")
                            {
                                score += 50;
                            }
                            else if (enemy[i].id == "sloth")
                            {
                                score += 100;
                            }
                            enemiesKilled++;
                            enemy.RemoveAt(i);
                            totalEnemyCount--;
                        }

                        else
                        {
                            foreach (Weapon w in p.weapons)
                            {
                                enemy[i].health_points -= w.Damage;
                            }
                        }
                        break;
                    }
                }
            }
        }

        public void EnemyAttackCollision()
        {
            foreach (Enemy e in enemy)
            {
                if (Vector2.Distance(pLocation, e.position) < 35)
                {
                    if (playerHP == 0)
                    {
                        Die();
                    }

                    else
                    {
                        playerHP--;
                    }
                }
            }

            foreach (Enemy e in enemy)
            {
                if (Vector2.Distance(pLocation2, e.position) < 35)
                {
                    if (playerHP2 == 0)
                    {
                        Die();
                    }

                    else
                    {
                        playerHP2--;
                    }
                }
            }
        }

        public void getUserInput(GameTime gameTime)
        {
            //Player Items
            if (currentKeyboard.IsKeyDown(Keys.G) && !previousKeyboard.IsKeyDown(Keys.G))
                PickUpWeapon();
            if (currentKeyboard.IsKeyDown(Keys.OemQuotes) && !previousKeyboard.IsKeyDown(Keys.OemQuotes))
                PickUpWeapon2();

            //Player Moving
            if (timeSinceLastUpdate > 56)
            {
                timeSinceLastUpdate = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.A) || (Keyboard.GetState().IsKeyDown(Keys.D)))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        isFacingLeft = true;
                        isRunning = true;
                        if (pLocation.X > 0)
                        {
                            pLocation.X -= 5;
                        }

                        p.Animate();
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        isRunning = true;
                        isFacingLeft = false;
                        if (pLocation.X < graphics.PreferredBackBufferWidth - 75)
                        {
                            pLocation.X += 5;
                        }
                        p.Animate();
                    }
                }

                else
                {
                    isRunning = false;
                }


                if (Keyboard.GetState().IsKeyDown(Keys.H) || (Keyboard.GetState().IsKeyDown(Keys.K)))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.H))
                    {
                        isFacingLeft2 = true;
                        isRunning2 = true;
                        if (pLocation2.X > 0)
                        {
                            pLocation2.X -= 5;
                        }

                        p2.Animate();
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.K))
                    {
                        isRunning2 = true;
                        isFacingLeft2 = false;
                        if (pLocation2.X < graphics.PreferredBackBufferWidth)
                        {
                            pLocation2.X += 5;
                        }
                        p2.Animate();
                    }
                }

                else
                {
                    isRunning2 = false;
                }
            }


            //Player Attacking
            if (currentKeyboard.IsKeyDown(Keys.LeftShift) && !previousKeyboard.IsKeyDown(Keys.LeftShift))
            {
                isAttacking = true;
                if (playerAttackTime < 0)
                {
                    p.Attack();
                    playerAttackTime = playerAttackRate;
                }

                if (fireTime <= 0)
                {
                    if (p.CurrentWeapon != null)
                    {
                        fireTime = p.CurrentWeapon.fireRate;

                        var b = new Bullet(bulletTexture, new Vector2());

                        if (isFacingLeft)
                        {
                            b.position.X = pLocation.X + 25;
                            b.position.Y = pLocation.Y + 50;
                            b.velocity = new Vector2(-p.CurrentWeapon.bulletMoveRate, 0);
                        }

                        else
                        {
                            b.position.X = pLocation.X + 60;
                            b.position.Y = pLocation.Y + 50;
                            b.velocity = new Vector2(p.CurrentWeapon.bulletMoveRate, 0);
                        }
                        bullets.Add(b);
                    }
                }
            }
            else
            {
                isAttacking = false;
            }

            if (currentKeyboard.IsKeyDown(Keys.V) && !previousKeyboard.IsKeyDown(Keys.V))
            {
                isAttacking2 = true;
                if (playerAttackTime2 < 0)
                {
                    p2.Attack();
                    playerAttackTime2 = playerAttackRate;
                }

                if (fireTime2 <= 0)
                {
                    if (p2.CurrentWeapon != null)
                    {
                        fireTime2 = p2.CurrentWeapon.fireRate;

                        var b2 = new Bullet(bulletTexture, new Vector2());

                        if (isFacingLeft2)
                        {
                            b2.position.X = pLocation2.X + 25;
                            b2.position.Y = pLocation2.Y + 50;
                            b2.velocity = new Vector2(-p2.CurrentWeapon.bulletMoveRate, 0);
                        }

                        else
                        {
                            b2.position.X = pLocation2.X + 60;
                            b2.position.Y = pLocation2.Y + 50;
                            b2.velocity = new Vector2(p2.CurrentWeapon.bulletMoveRate, 0);
                        }
                        bullets.Add(b2);
                    }
                }
            }
            else
            {
                isAttacking2 = false;
            }
        }

        private void SpawnItem()
        {
            if (enemiesKilled == 3)
            {
                enemiesKilled = 0;

                int weapon_type = r.Next(2);
                Weapon w = null;

                var wbPosition = new Vector2(r.Next(40, 500), -100);

                switch (weapon_type)
                {
                    case 0:
                        w = new Cannon(40, 10, cannonBox, wbPosition);
                        break;
                    case 1:
                        w = new Splicer(20, 10, splicerBox, wbPosition);
                        break;
                }

                weaponBox.Add(w);
            }

            foreach (Weapon wb in weaponBox)
            {
                float wbVelocity;
                wbVelocity = 2;

                if (wb.Position.Y == graphics.PreferredBackBufferHeight - 200)
                {
                    wbVelocity = 0;
                }

                else
                {
                    wb.position.Y += wbVelocity;
                }
            }
        }

        private void SpawnEnemy()
        {
            if (timeSinceLastSpawn < 5)
            {
                timeSinceLastSpawn = 0;
                if (totalEnemyCount < maxEnemyCount)
                {
                    for (int i = 0; i < maxEnemyCount; i++)
                    {
                        int enemy_type = r.Next(3);
                        Enemy e = null;

                        var ePosition = new Vector2(r.Next(graphics.PreferredBackBufferWidth + 100, 1500),
                            graphics.PreferredBackBufferHeight - 200);
                        switch (enemy_type)
                        {
                            case 0:
                                e = new Zombie(zombieTexture, ePosition, 10);
                                break;
                            case 1:
                                e = new Stark(starkTexture, ePosition, 50);
                                break;
                            case 2:
                                e = new Sloth(slothTexture, ePosition, 80);
                                break;
                        }
                        enemy.Add(e);
                        totalEnemyCount++;
                    }
                }
            }
        }

        private void ChangeWeapon()
        {
            if (currentKeyboard.IsKeyDown(Keys.Q) && !previousKeyboard.IsKeyDown(Keys.Q))
                p.PreviousWeapon();
            if (currentKeyboard.IsKeyDown(Keys.E) && !previousKeyboard.IsKeyDown(Keys.E))
                p.NextWeapon();
            if (currentKeyboard.IsKeyDown(Keys.Y) && !previousKeyboard.IsKeyDown(Keys.Y))
                p2.PreviousWeapon();
            if (currentKeyboard.IsKeyDown(Keys.I) && !previousKeyboard.IsKeyDown(Keys.I))
                p2.NextWeapon();
        }

        private void DropWeapon()
        {
            for (int i = p.weapons.Count - 1; i >= 0; i--)
            {
                if (p.weapons[i].ammo == 0)
                {
                    p.weapons.RemoveAt(i);
                    p.NextWeapon();
                }
            }

            for (int i = p2.weapons.Count - 1; i >= 0; i--)
            {
                if (p2.weapons[i].ammo == 0)
                {
                    p2.weapons.RemoveAt(i);
                    p2.NextWeapon();
                }
            }
        }

        private void PickUpWeapon()
        {
            for (int i = weaponBox.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(pLocation, weaponBox[i].Position) < itemCollisionDistance)
                {
                    if (weaponBox[i].weaponID == "Cannon")
                    {
                        p.AddWeapon(new Cannon(40, 20, pCannonTexture, pLocation));
                    }
                    else if (weaponBox[i].weaponID == "Splicer")
                    {
                        p.AddWeapon(new Splicer(20, 15, pSplicerTexture, pLocation));
                    }
                    else
                    {
                        return;
                    }
                    weaponBox.RemoveAt(i);
                }
            }
        }

        private void PickUpWeapon2()
        {
            for (int i = weaponBox.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(pLocation2, weaponBox[i].Position) < itemCollisionDistance)
                {
                    if (weaponBox[i].weaponID == "Cannon")
                    {
                        p2.AddWeapon(new Cannon(40, 20, pCannonTexture, pLocation2));
                    }
                    else if (weaponBox[i].weaponID == "Splicer")
                    {
                        p2.AddWeapon(new Splicer(20, 50, pSplicerTexture, pLocation2));
                    }
                    else
                    {
                        return;
                    }
                    weaponBox.RemoveAt(i);
                }
            }
        }

        private void MoveBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Move();
                if ((bullets[i].position.X < -10) ||
                    (bullets[i].position.X > graphics.PreferredBackBufferWidth))
                    bullets.RemoveAt(i);
            }
        }

        private void Die()
        {
            if (playerHP == 0 || playerHP2 == 0)
            {
                gameIsOver = true;
            }
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            DrawScreens();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawScreens()
        {
            if (currentGameState == GameScreen.title)
            {
                spriteBatch.Draw(title, new Rectangle(0, 0, title.Width, title.Height), Color.White);
                titleMenu.Draw(spriteBatch);
            }

            else if (currentGameState == GameScreen.singlePlayer || currentGameState == GameScreen.multiPlayer)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, 1024), Color.White);
                DrawEnemy();
                DrawPlayer();
                DrawItem();
                DrawHUD();
            }

            else if (currentGameState == GameScreen.instructions)
            {
                spriteBatch.Draw(instructions, new Rectangle(0, 0, instructions.Width, instructions.Height),
                    Color.White);
            }

            else if (currentGameState == GameScreen.highScores)
            {
                spriteBatch.Draw(highscore, new Rectangle(0, 0, highscore.Width, highscore.Height), Color.White);
                DrawHighScores();
            }

            if (currentGameState == GameScreen.enterInitials)
            {
                spriteBatch.DrawString(font, highScoreMsg, new Vector2(364, 212), Color.White, 0, new Vector2(0, 0),
                    1f, SpriteEffects.None, 0);

                if (name != null)
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        spriteBatch.DrawString(font, name[i].ToString(), new Vector2(364, 212), Color.White, 0,
                            new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                    }
                }
            }
        }

        private void DrawEnemy()
        {
            foreach (Enemy e in enemy)
            {
                if (e.Position.X < pLocation.X)
                {
                    e.isFacingLeft = false;
                    spriteBatch.Draw(e.Texture, new Rectangle((int) e.Position.X, (int) e.Position.Y, 75, 75),
                        new Rectangle(e.cellIndex*75, 0, 75, 75), Color.White, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    e.isFacingLeft = true;
                    spriteBatch.Draw(e.Texture, e.Position, new Rectangle(e.cellIndex*75, 0, 75, 75), Color.White);
                }
            }
        }


        private void DrawPlayer()
        {
            if (p.CurrentWeapon != null)
            {
                foreach (Bullet b in bullets)
                {
                    spriteBatch.Draw(b.Texture, b.position, Color.Black);
                }

                if (isFacingLeft)
                {
                    spriteBatch.Draw(p.CurrentWeapon.Texture,
                        new Rectangle((int) pLocation.X, (int) pLocation.Y, 75, 75),
                        new Rectangle(p.cellIndex*75, 0, 75, 75), Color.White, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    spriteBatch.Draw(p.CurrentWeapon.Texture, pLocation, new Rectangle(p.cellIndex*75, 0, 75, 75),
                        Color.White);
                }
            }

            else
            {
                if (isFacingLeft)
                {
                    spriteBatch.Draw(pTexture, new Rectangle((int) pLocation.X, (int) pLocation.Y, 75, 75),
                        new Rectangle(p.cellIndex*75, 0, 75, 75), Color.White, 0, new Vector2(0, 0),
                        SpriteEffects.FlipHorizontally, 0);
                }

                else
                {
                    spriteBatch.Draw(pTexture, pLocation, new Rectangle(p.cellIndex*75, 0, 75, 75), Color.White);
                }
            }

            if (currentGameState == GameScreen.multiPlayer)
            {
                if (p2.CurrentWeapon != null)
                {
                    foreach (Bullet b in bullets)
                    {
                        spriteBatch.Draw(b.Texture, b.position, Color.Black);
                    }

                    if (isFacingLeft2)
                    {
                        spriteBatch.Draw(p2.CurrentWeapon.Texture,
                            new Rectangle((int) pLocation2.X, (int) pLocation2.Y, 75, 75),
                            new Rectangle(p2.cellIndex*75, 0, 75, 75), Color.White, 0, new Vector2(0, 0),
                            SpriteEffects.FlipHorizontally, 0);
                    }

                    else
                    {
                        spriteBatch.Draw(p2.CurrentWeapon.Texture, pLocation2,
                            new Rectangle(p2.cellIndex*75, 0, 75, 75), Color.White);
                    }
                }

                else
                {
                    if (isFacingLeft2)
                    {
                        spriteBatch.Draw(pTexture, new Rectangle((int) pLocation2.X, (int) pLocation2.Y, 75, 75),
                            new Rectangle(p2.cellIndex*75, 0, 75, 75), Color.White, 0, new Vector2(0, 0),
                            SpriteEffects.FlipHorizontally, 0);
                    }

                    else
                    {
                        spriteBatch.Draw(pTexture, pLocation2, new Rectangle(p2.cellIndex*75, 0, 75, 75),
                            Color.White);
                    }
                }
            }
        }

        private void DrawItem()
        {
            foreach (Weapon w in weaponBox)
            {
                spriteBatch.Draw(w.Texture, w.Position, Color.White);
            }
        }

        private void DrawHUD()
        {
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(20, graphics.PreferredBackBufferHeight - 50),
                Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "P1 - Health: " + playerHP,
                new Vector2(160, graphics.PreferredBackBufferHeight - 50), Color.White, 0, new Vector2(0, 0), 0.5f,
                SpriteEffects.None, 0);
            if (p.CurrentWeapon != null)
            {
                spriteBatch.DrawString(font, "P1 - " + p.CurrentWeapon.weaponID + " Ammo : " + p.CurrentWeapon.ammo,
                    new Vector2(160, graphics.PreferredBackBufferHeight - 30), Color.White, 0, new Vector2(0, 0),
                    0.5f, SpriteEffects.None, 0);
            }

            if (currentGameState == GameScreen.multiPlayer)
            {
                spriteBatch.DrawString(font, "P2 - Health: " + playerHP2,
                    new Vector2(400, graphics.PreferredBackBufferHeight - 50), Color.White, 0, new Vector2(0, 0),
                    0.5f, SpriteEffects.None, 0);
                if (p2.CurrentWeapon != null)
                {
                    spriteBatch.DrawString(font,
                        "P2 - " + p2.CurrentWeapon.weaponID + " Ammo : " + p2.CurrentWeapon.ammo,
                        new Vector2(400, graphics.PreferredBackBufferHeight - 30), Color.White, 0, new Vector2(0, 0),
                        0.5f, SpriteEffects.None, 0);
                }
            }
        }

        private void DrawHighScores()
        {
            HighScoreData data = LoadHighScores(HighScoresFilename);
            for (int i = 0; i < data.Count; i++)
            {
                spriteBatch.DrawString(font, data.playerName[0] + ":" + " " + data.highScore[0],
                    new Vector2(164, 212), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, data.playerName[1] + ":" + " " + data.highScore[1],
                    new Vector2(164, 262), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, data.playerName[2] + ":" + " " + data.highScore[2],
                    new Vector2(164, 312), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, data.playerName[3] + ":" + " " + data.highScore[3],
                    new Vector2(164, 362), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, data.playerName[4] + ":" + " " + data.highScore[4],
                    new Vector2(164, 412), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
            }
        }

        private enum GameScreen
        {
            title,
            instructions,
            singlePlayer,
            multiPlayer,
            highScores,
            enterInitials
        }

        /// <summary>
        ///     This is the main type for your game
        /// </summary>
        [Serializable]
        public struct HighScoreData
        {
            public int Count;
            public int[] highScore;
            public string[] playerName;

            public HighScoreData(int count)
            {
                playerName = new string[count];
                highScore = new int[count];

                Count = count;
            }
        }
    }
}