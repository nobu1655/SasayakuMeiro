public static class Score
{
    public static string CalculateRank(float timeLeft)
    {
        if (timeLeft >= 50f) return "S";
        if (timeLeft >= 40f) return "A";
        if (timeLeft >= 20f) return "B";
        return "C";
    }
}
