#include "StdAfx.h"
#include "PubSubManager.h"

PubSubManager::PubSubManager(void)
{
	redisCtx_ = NULL;

	FD_ZERO(&readfds_);
	FD_ZERO(&writefds_);
	FD_ZERO(&exceptfds_);
}

PubSubManager::~PubSubManager(void)
{
}

void PubSubManager::OnTimer()
{
	if (!redisCtx_)
		return;

	timeval tv;
	FD_SET(redisCtx_->c.fd, &readfds_);
	FD_SET(redisCtx_->c.fd, &writefds_);
	FD_SET(redisCtx_->c.fd, &exceptfds_);

	int i = select(redisCtx_->c.fd + 1, &readfds_, &writefds_, &exceptfds_, NULL);
	if (-1 == i)
		return;

	if ( FD_ISSET(redisCtx_->c.fd, &readfds_) )
	{
		redisAsyncHandleRead(redisCtx_);
	}
	if ( FD_ISSET(redisCtx_->c.fd, &writefds_) )
	{
		redisAsyncHandleWrite(redisCtx_);
	}
	if ( FD_ISSET(redisCtx_->c.fd, &exceptfds_) )
	{
		redisAsyncDisconnect(redisCtx_);
		redisCtx_ = NULL;

		DEBUGLOG(">> exceptfds_ error");
	}
}

bool PubSubManager::Connect( const char *ipaddr, int port )
{
	if (!ipaddr)
		return false;

	if (redisCtx_)
	{
		redisAsyncDisconnect(redisCtx_);
		redisCtx_ = NULL;
		DEBUGLOG(">> Disconnected");
		return true;
	}

	redisCtx_ = redisAsyncConnect(ipaddr, port);
	if (redisCtx_ && 0 == redisCtx_->err)
	{
		DEBUGLOG(">> Connected to %s:%d", ipaddr, port);
		return true;
	}

	return false;
}

bool PubSubManager::Subscribe( const char *channel )
{
	CString cmd;
	const char *op;

	if (!redisCtx_ || !channel)
		return false;

	if ( strchr(channel, '*') )
		op = "PSUBSCRIBE";
	else
		op = "SUBSCRIBE";

	cmd.Format("%s %s", op, channel);
	DEBUGLOG(cmd);

	return ( REDIS_OK == redisAsyncCommand(redisCtx_, RedisCallback, this, cmd) );
}

bool PubSubManager::Unsubscribe( const char *channel )
{
	CString cmd;
	const char *op;

	if (!redisCtx_ || !channel)
		return false;

	if ( strchr(channel, '*') )
		op = "PUNSUBSCRIBE";
	else
		op = "UNSUBSCRIBE";

	cmd.Format("%s %s", op, channel);
	DEBUGLOG(cmd);

	return ( REDIS_OK == redisAsyncCommand(redisCtx_, RedisCallback, this, cmd) );
}


bool PubSubManager::Publish( const char *channel, const char *message )
{
	CString cmd;

	if (!redisCtx_ || !channel || !message)
		return false;

	cmd.Format("PUBLISH %s %s", channel, message);
	DEBUGLOG(cmd);

	return ( REDIS_OK == redisAsyncCommand(redisCtx_, RedisCallback, this, cmd) );
}

void PubSubManager::PrintReply( redisReply *reply )
{
	switch (reply->type)
	{
	case REDIS_REPLY_INTEGER:
		DEBUGLOG("[Integer] %d", reply->integer);
		break;

	case REDIS_REPLY_ERROR:
	case REDIS_REPLY_STRING:
		DEBUGLOG(reply->str);
		break;

	case REDIS_REPLY_ARRAY:
		if (reply->element)
		{
			for (int i = 0; i < reply->elements; i++)
			{
				PrintReply(reply->element[i]);
			}
		}
		break;
	}
}

void PubSubManager::RedisCallback( redisAsyncContext *ctx, void *reply, void *priv )
{
	if (!ctx || !reply || !priv)
		return;

	PubSubManager *This = (PubSubManager *)priv;

	redisReply *r = (redisReply *)reply;

	This->PrintReply(r);
}
