#pragma once

#include "async.h"

class PubSubManager
{
public:
	PubSubManager(void);
	virtual ~PubSubManager(void);

	void OnTimer();

	bool Connect(const char *ipaddr, int port);

	bool Subscribe(const char *channel);
	bool Unsubscribe(const char *channel);
	bool Publish(const char *channel, const char *message);

	static void RedisCallback(redisAsyncContext *ctx, void *reply, void *priv);

private:

	void PrintReply( redisReply *reply );

	redisAsyncContext *redisCtx_;

	fd_set readfds_, writefds_, exceptfds_;
};
