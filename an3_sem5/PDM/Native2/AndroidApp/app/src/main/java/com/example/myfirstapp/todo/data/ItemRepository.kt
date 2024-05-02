package com.example.myfirstapp.todo.data

import android.app.ActivityManager
import android.content.Context
import android.os.Handler
import android.os.Looper
import android.util.Log
import android.widget.Toast
import com.example.myfirstapp.core.Result
import com.example.myfirstapp.core.TAG
import com.example.myfirstapp.core.data.remote.Api
import com.example.myfirstapp.todo.data.remote.ItemEvent
import com.example.myfirstapp.todo.data.remote.ItemService
import com.example.myfirstapp.todo.data.remote.ItemWsClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.channels.BufferOverflow
import kotlinx.coroutines.channels.awaitClose
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.MutableSharedFlow
import kotlinx.coroutines.flow.callbackFlow
import kotlinx.coroutines.withContext


class ItemRepository(private val itemService: ItemService, private val itemWsClient: ItemWsClient,
        private val database: GameDatabase, private val context: Context
) {
    private var games: List<Game> = listOf();


    private var itemsFlow: MutableSharedFlow<Result<List<Game>>> = MutableSharedFlow(
        replay = 1,
        onBufferOverflow = BufferOverflow.DROP_OLDEST
    )

    val gameStream: Flow<Result<List<Game>>> = itemsFlow

    init {
        Log.d(TAG, "init")
    }

    suspend fun refresh() {
        Log.d(TAG, "refresh started")
        try {
            games = itemService.find(authorization = getBearerToken())
            database.gameDao().clear()
            for(game in games) {
                game.requiresUpdate = false
                game.requiresCreate = false
                database.gameDao().insert(game)
            }
            Log.d(TAG, "refresh succeeded")
            itemsFlow.emit(Result.Success(games))
        } catch (e: Exception) {
            Log.d(TAG, "refresh failed", e)
            games  =database.gameDao().getAll()
            itemsFlow.emit(Result.Success(games))
            //itemsFlow.emit(Result.Error(e))
        }
    }

    suspend fun openWsClient() {
        Log.d(TAG, "openWsClient")
        withContext(Dispatchers.IO) {
            getItemEvents().collect {
                Log.d(TAG, "Item event collected $it")
                if (it is Result.Success) {
                    val itemEvent = it.data;
                    when (itemEvent.event) {
                        "created" -> handleItemCreated(itemEvent.payload.updatedGame)
                        "updated" -> handleItemUpdated(itemEvent.payload.updatedGame)
                        "deleted" -> handleItemDeleted(itemEvent.payload.updatedGame)
                    }
                }
            }
        }
    }

    suspend fun closeWsClient() {
        Log.d(TAG, "closeWsClient")
        withContext(Dispatchers.IO) {
            itemWsClient.closeSocket()
        }
    }

    suspend fun getItemEvents(): Flow<Result<ItemEvent>> = callbackFlow {
        Log.d(TAG, "getItemEvents started")
        itemWsClient.openSocket(
            onEvent = {
                Log.d(TAG, "onEvent $it")
                if (it != null) {
                    Log.d(TAG, "onEvent trySend $it")
                    trySend(Result.Success(it))
                }
            },
            onClosed = { close() },
            onFailure = { close() });
        awaitClose { itemWsClient.closeSocket() }
    }

    suspend fun update(game: Game): Game {
        try {
            game.requiresUpdate=false
            Log.d(TAG, "update $game...")
            val updatedItem = itemService.update(authorization = getBearerToken(), game._id, game)
            Log.d(TAG, "update $game succeeded")
            handleItemUpdated(updatedItem)
            return updatedItem
        }
        catch (ex:Exception){
            Log.d(TAG, "failed update $game")
            game.requiresUpdate=true
            handleItemUpdated(game)

            Handler(Looper.getMainLooper()).post({
                Toast.makeText(context, "Server unreachable. Saved locally", Toast.LENGTH_LONG).show()
            })
            return game
        }
    }

    suspend fun save(game: Game): Game {
        try {
            Log.d(TAG, "save $game...")
            game.requiresCreate=false
            val createdItem = itemService.create(authorization = getBearerToken(), game)
            Log.d(TAG, "save $game succeeded")
            Log.d(TAG, "handle created $createdItem")
            handleItemCreated(createdItem)
            return createdItem
        }
        catch (ex:Exception){
            val createdItem = Game(
                title = game.title,
                lastVersion = game.lastVersion,
                url=game.url,
                totalReleases = game.totalReleases,
                isOpenSource = game.isOpenSource,
                platform = game.platform,
                releaseDate = game.releaseDate,
                imageUrl = game.imageUrl,
                requiresCreate = true,
                requiresUpdate = false
            )
            Log.d(TAG, "failed create on the server $game")
            handleItemCreated(createdItem)

            Handler(Looper.getMainLooper()).post({
                Toast.makeText(context, "Server unreachable. Saved locally", Toast.LENGTH_LONG).show()
            })
            return createdItem
        }
    }

    private suspend fun handleItemDeleted(game: Game) {
        Log.d(TAG, "handleItemDeleted - todo $game")
    }

    private suspend fun handleItemUpdated(game: Game) {
        Log.d(TAG, "handleItemUpdated...: $game")
        games = games.map { if (it._id == game._id) game else it }
        database.gameDao().update(game)
        itemsFlow.emit(Result.Success(games))
    }

    private suspend fun handleItemCreated(game: Game) {
        Log.d(TAG, "handleItemCreated...: $game")
        if(!games.contains(game)) {
            games = games.plus(game)
            database.gameDao().insert(game)
        }
        itemsFlow.emit(Result.Success(games))
    }

    fun quite_remove(game:Game){
        games = games.minus(game)
        database.gameDao().deleteById(game._id)
    }

    fun setToken(token: String) {
        itemWsClient.authorize(token)
    }

    private fun getBearerToken() = "Bearer ${Api.tokenInterceptor.token}"
}