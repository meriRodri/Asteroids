namespace Code.Managers
{
    public static class General
    {
        public static float MaxX;
        public static float MaxY;

        public static string BulletTag = "Bullet";
        public static string AsteroidTag = "Asteroid";
        public static string PlayerTag = "Player";

        public static int TimeBetweenWaves = 5;
        public static int TimeRestart = 5;

        public static State GameState = State.Start;
    }

    public enum State
    {
        Start,
        Playing
    }
}
