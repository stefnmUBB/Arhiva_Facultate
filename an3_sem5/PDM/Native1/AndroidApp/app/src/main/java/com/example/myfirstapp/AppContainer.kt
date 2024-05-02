package com.example.myfirstapp

import android.content.Context
import android.util.Log
import androidx.datastore.preferences.preferencesDataStore
import com.example.myfirstapp.auth.data.remote.AuthDataSource
import com.example.myfirstapp.core.TAG
import com.example.myfirstapp.auth.data.AuthRepository
import com.example.myfirstapp.core.data.UserPreferencesRepository
import com.example.myfirstapp.core.data.remote.Api
import com.example.myfirstapp.todo.data.ItemRepository
import com.example.myfirstapp.todo.data.remote.ItemService
import com.example.myfirstapp.todo.data.remote.ItemWsClient

val Context.userPreferencesDataStore by preferencesDataStore(
    name = "user_preferences"
)

class AppContainer(val context: Context) {
    init {
        Log.d(TAG, "init")
    }

    private val authDataSource: AuthDataSource = AuthDataSource()

    val itemService: ItemService = Api.retrofit.create(ItemService::class.java)
    val itemWsClient: ItemWsClient = ItemWsClient(Api.okHttpClient)

    val itemRepository: ItemRepository by lazy {
        ItemRepository(itemService, itemWsClient)
    }

    val authRepository: AuthRepository by lazy {
        AuthRepository(authDataSource)
    }

    val userPreferencesRepository: UserPreferencesRepository by lazy {
        UserPreferencesRepository(context.userPreferencesDataStore)
    }
}