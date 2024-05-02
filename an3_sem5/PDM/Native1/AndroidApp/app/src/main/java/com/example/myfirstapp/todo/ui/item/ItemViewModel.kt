package com.example.myfirstapp.todo.ui.item

import android.util.Log
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.viewModelScope
import androidx.lifecycle.viewmodel.initializer
import androidx.lifecycle.viewmodel.viewModelFactory
import com.example.myfirstapp.MyFirstApplication
import com.example.myfirstapp.core.Result
import com.example.myfirstapp.core.TAG
import com.example.myfirstapp.todo.data.Game
import com.example.myfirstapp.todo.data.ItemRepository
import kotlinx.coroutines.launch
import java.util.Date

data class ItemUiState(
    val itemId: String? = null,
    val game: Game = Game(),
    var loadResult: Result<Game>? = null,
    var submitResult: Result<Game>? = null,
)

class ItemViewModel(private val itemId: String?, private val itemRepository: ItemRepository) :
    ViewModel() {

    var uiState: ItemUiState by mutableStateOf(ItemUiState(loadResult = Result.Loading))
        private set

    init {
        Log.d(TAG, "init")
        if (itemId != null) {
            loadItem()
        } else {
            uiState = uiState.copy(loadResult = Result.Success(Game()))
        }
    }

    fun loadItem() {
        viewModelScope.launch {
            itemRepository.gameStream.collect { result ->
                if (!(uiState.loadResult is Result.Loading)) {
                    return@collect
                }
                if (result is Result.Success) {
                    val items = result.data
                    val game = items.find { it._id == itemId } ?: Game()
                    uiState = uiState.copy(loadResult = Result.Success(game), game = game)
                } else if (result is Result.Error) {
                    uiState =
                        uiState.copy(loadResult = Result.Error(result.exception))
                }
            }
        }
    }

    fun saveItem(title: String, url:String, totalReleases:Int, isOpenSource: Boolean, platform:String, releaseDate: Date, lastVersion:String){
        viewModelScope.launch {
            Log.d(TAG, "save new game!!!");
            try{
                uiState = uiState.copy(submitResult = Result.Loading)
                val item = uiState.game.copy(title=title, url=url, totalReleases = totalReleases, isOpenSource = isOpenSource, platform=platform
                    , releaseDate = convertDateToString(releaseDate), lastVersion = lastVersion)
                val savedGame: Game = itemRepository.save(item)
                Log.d(TAG, "save game succeeeded!!!!");
                uiState = uiState.copy(submitResult = Result.Success(savedGame))
            }catch (e: Exception){
                Log.d(TAG, "saveOrUpdateItem failed");
                uiState = uiState.copy(submitResult = Result.Error(e))
            }
        }
    }

    fun UpdateItem(title: String, url:String, totalReleases:Int, isOpenSource: Boolean, platform:String,releaseDate: Date, lastVersion:String) {
        viewModelScope.launch {
            Log.d(TAG, "update game!!!");
            try {
                uiState = uiState.copy(submitResult = Result.Loading)
                val item = uiState.game.copy(title=title, url=url, totalReleases = totalReleases, isOpenSource = isOpenSource,
                    platform=platform, releaseDate = convertDateToString(releaseDate), lastVersion = lastVersion)
                val savedGame: Game = itemRepository.update(item)
                Log.d(TAG, "UpdateItem succeeeded");
                uiState = uiState.copy(submitResult = Result.Success(savedGame))
            } catch (e: Exception) {
                Log.d(TAG, "saveOrUpdateItem failed");
                uiState = uiState.copy(submitResult = Result.Error(e))
            }
        }
    }

    companion object {
        fun Factory(itemId: String?): ViewModelProvider.Factory = viewModelFactory {
            initializer {
                val app =
                    (this[ViewModelProvider.AndroidViewModelFactory.APPLICATION_KEY] as MyFirstApplication)
                ItemViewModel(itemId, app.container.itemRepository)
            }
        }
    }
}
