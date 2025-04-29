namespace LearnKana.Shared.Extensions
{
    public static class RandomExtensions
    {
        public static T GetRandomItem<T>(this IReadOnlyList<T> list, Random? random = null)
        {
            random ??= Random.Shared;
            int index = random.Next(list.Count);
            return list[index];
        }

        /// <summary>
        /// Fisher-Yates algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T value = list[j];
                list[j] = list[i];
                list[i] = value;
            }
        }

        /// <summary>
        /// Generates a random value of either 0 or 1.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static int HeadsOrTails(this Random random)
        {
            return random.Next(2);
        }
    }
}
