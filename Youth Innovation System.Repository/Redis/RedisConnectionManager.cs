using Youth_Innovation_System.Core.IRedis;
using Youth_Innovation_System.Core.Shared.Enums;

namespace Youth_Innovation_System.Repository.Redis
{
    public class RedisConnectionManager : IRedisConnectionManager
    {
        private readonly IRedisHelper _redisHelper;

        public RedisConnectionManager(IRedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }

        public async Task AddConnectionAsync(string userId, string connectionId)
          => await _redisHelper.SetValueAsync(userId, connectionId);

        public async Task<string> GetConnectionAsync(string userId)
         => await _redisHelper.GetValueAsync(userId);
        public async Task RemoveConnectionAsync(string userId)
         => await _redisHelper.RemoveValueAsync(userId);
        public async Task SetUserOnlineAsync(string userId)
        => await _redisHelper.SetValueAsync($"user_{userId}", nameof(UserStatus.Online));

        public async Task SetUserOfflineAsync(string userId)
        => await _redisHelper.RemoveValueAsync($"user_{userId}");
        public async Task<bool> IsUserOnlineAsync(string userId)
        => await _redisHelper.GetValueAsync($"user_{userId}") == nameof(UserStatus.Online);
    }
}
