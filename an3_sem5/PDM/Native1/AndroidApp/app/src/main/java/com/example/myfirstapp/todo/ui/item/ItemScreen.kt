package com.example.myfirstapp.todo.ui.item

import android.util.Log
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.Checkbox
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.LinearProgressIndicator
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.TopAppBar
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewmodel.compose.viewModel
import com.example.myfirstapp.R
import com.example.myfirstapp.core.Result

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ItemScreen(itemId: String?, onClose: () -> Unit) {
    val itemViewModel = viewModel<ItemViewModel>(factory = ItemViewModel.Factory(itemId))
    val itemUiState = itemViewModel.uiState

    var title by rememberSaveable { mutableStateOf(itemUiState.game.title) }
    var url by rememberSaveable { mutableStateOf(itemUiState.game.url) }
    var totalReleases by rememberSaveable { mutableStateOf(itemUiState.game.totalReleases) }
    var lastVersion by rememberSaveable { mutableStateOf(itemUiState.game.lastVersion) }
    var isOpenSource by rememberSaveable { mutableStateOf(itemUiState.game.isOpenSource) }
    var platform by rememberSaveable { mutableStateOf(itemUiState.game.platform) }
    var releaseDate by rememberSaveable { mutableStateOf(convertStringToDate(itemUiState.game.releaseDate)) }

    Log.d("ItemScreen", "recompose, text = $title")

    LaunchedEffect(itemUiState.submitResult) {
        Log.d("ItemScreen", "Submit = ${itemUiState.submitResult}");
        if (itemUiState.submitResult is Result.Success) {
            Log.d("ItemScreen", "Closing screen");
            onClose();
        }
    }

    var textInitialized by remember { mutableStateOf(itemId == null) }
    LaunchedEffect(itemId, itemUiState.loadResult) {
        Log.d("ItemScreen", "Text initialized = ${itemUiState.loadResult}");
        if (textInitialized) {
            return@LaunchedEffect
        }
        if (!(itemUiState.loadResult is Result.Loading)) {
            title = itemUiState.game.title
            totalReleases = itemUiState.game.totalReleases
            platform=itemUiState.game.platform
            isOpenSource=itemUiState.game.isOpenSource
            url=itemUiState.game.url
            lastVersion=itemUiState.game.lastVersion
            releaseDate= convertStringToDate(itemUiState.game.releaseDate)
            textInitialized = true
        }
    }

    val defaultFieldModifier = Modifier.padding(vertical = 5.dp)

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text(text = stringResource(id = R.string.item)) },
                actions = {
                    Button(onClick = {
                        Log.d("ItemScreen", "back to list");
                        onClose()
                    }) { Text("Back") }
                    Button(onClick = {
                        Log.d("ItemScreen", "save item text = $title");
                        itemViewModel.UpdateItem(title,url, totalReleases, isOpenSource, platform, releaseDate, lastVersion)
                    }, modifier=Modifier.padding(horizontal=8.dp)) { Text("Update") }
                }
            )
        }
    ) {
        Column(
            modifier = Modifier
                .padding(it)
                .fillMaxSize()
        ) {
            if (itemUiState.loadResult is Result.Loading) {
                CircularProgressIndicator()
                return@Scaffold
            }
            if (itemUiState.submitResult is Result.Loading) {
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.CenterHorizontally
                ) { LinearProgressIndicator() }
            }
            if (itemUiState.loadResult is Result.Error) {
                Text(text = "Failed to load item - ${(itemUiState.loadResult as Result.Error).exception?.message}")
            }
            Column {
                TextField(
                    value = title,
                    onValueChange = { title = it }, label = { Text("Title") },
                    modifier = defaultFieldModifier.fillMaxWidth(),
                )

                PlatformCombobox(
                    value=platform,
                    onValueChange= { platform=it })

                TextField(
                    value = lastVersion,
                    onValueChange = { lastVersion = it }, label = { Text("Last Version") },
                    modifier = defaultFieldModifier.fillMaxWidth(),
                )

                MyNumberField(
                    value = totalReleases,
                    onValueChanged = {totalReleases=it},
                    modifier = defaultFieldModifier,
                    label = "Total releases"
                )

                TextField(
                    value = url,
                    onValueChange = { url = it }, label = { Text("Url") },
                    modifier = defaultFieldModifier.fillMaxWidth(),
                )

                MyDatePickerDialog(convertDateToString(releaseDate), {
                    releaseDate = convertStringToDate(it)
                }, label="Release date", modifier = defaultFieldModifier)

                Row(verticalAlignment = Alignment.CenterVertically, modifier = defaultFieldModifier) {
                    Checkbox(
                        checked = isOpenSource,
                        onCheckedChange = { isOpenSource = it }
                    )
                    Text(
                        text="Open source",
                    )
                }
            }
            if (itemUiState.submitResult is Result.Error) {
                Text(
                    text = "Failed to submit item - ${(itemUiState.submitResult as Result.Error).exception?.message}",
                    modifier = Modifier.fillMaxWidth(),
                )
            }
        }
    }
}


@Preview
@Composable
fun PreviewItemScreen() {
    ItemScreen(itemId = "0", onClose = {})
}
