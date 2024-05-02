package com.example.myfirstapp.todo.ui.item

import android.os.Build
import android.util.Log
import android.widget.CheckBox
import androidx.annotation.RequiresApi
import androidx.compose.foundation.clickable
import androidx.compose.foundation.focusable
import androidx.compose.foundation.interaction.MutableInteractionSource
import androidx.compose.foundation.interaction.PressInteraction
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.defaultMinSize
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.ArrowDropDown
import androidx.compose.material.icons.filled.ArrowDropUp
import androidx.compose.material3.Button
import androidx.compose.material3.Checkbox
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.DatePicker
import androidx.compose.material3.DatePickerState
import androidx.compose.material3.DisplayMode
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.ExposedDropdownMenuBox
import androidx.compose.material3.Icon
import androidx.compose.material3.LinearProgressIndicator
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.RadioButton
import androidx.compose.material3.RadioButtonColors
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.TopAppBar
import androidx.compose.material3.rememberDatePickerState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.lifecycle.viewmodel.compose.viewModel
import com.example.myfirstapp.core.Result
import androidx.compose.runtime.remember
import androidx.compose.ui.geometry.Size
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.layout.onGloballyPositioned
import androidx.compose.ui.platform.LocalDensity
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.toSize
import androidx.navigation.compose.rememberNavController
import com.example.myfirstapp.core.TAG
import java.text.SimpleDateFormat
import java.time.LocalDateTime
import java.util.Calendar
import java.util.Date
import java.util.Locale



@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ItemAddScreen(itemId: String?, onClose: () -> Unit){
    val itemViewModel = viewModel<ItemViewModel>(factory = ItemViewModel.Factory(itemId))
    val itemUiState = itemViewModel.uiState

    var title by rememberSaveable { mutableStateOf("") }
    var lastVersion by rememberSaveable { mutableStateOf("") }
    var platform by rememberSaveable { mutableStateOf("") }
    var url by rememberSaveable { mutableStateOf("") }
    var totalReleases by rememberSaveable { mutableStateOf(0) }
    var isOpenSource by rememberSaveable { mutableStateOf(false) }
    var releaseDate by rememberSaveable { mutableStateOf(Date()) }

    // transform in double la itemViewModel.update ....


    Log.d("ItemAddScreen", "recompose, text = $title")

    LaunchedEffect(itemUiState.submitResult) {
        Log.d("ItemScreen", "Submit = ${itemUiState.submitResult}");
        if (itemUiState.submitResult is Result.Success) {
            Log.d("ItemScreen", "Closing screen");
            onClose();
        }
    }

    val defaultFieldModifier = Modifier.padding(vertical = 5.dp)

    Scaffold (
        topBar = {
            TopAppBar(title = { Text(text = "Add Game") },
                    actions = {
                        Button(onClick = {
                            Log.d("ItemScreen", "back to list");
                            onClose()
                        }) { Text("Back") }

                        Button(onClick = {
                            Log.d("ItemScreen", "save item text = $title");
                            itemViewModel.saveItem(title, url, totalReleases, isOpenSource, platform, releaseDate, lastVersion, "")
                        }, modifier=Modifier.padding(horizontal=8.dp)) { Text("Save") }
                    })
        }
    )
    {
        val scrollState = rememberScrollState()
        Column(
            modifier = Modifier
                .padding(it)
                .fillMaxSize()
                .verticalScroll(state = scrollState)
        ){
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
            Column(modifier = Modifier.padding(horizontal=24.dp,vertical=0.dp)) {
                TextField(
                    value = title,
                    onValueChange = { title = it }, label = { Text("Title") },
                    modifier = defaultFieldModifier.fillMaxWidth(),
                )

                PlatformCombobox(
                    value=platform,
                    onValueChange= { platform=it },
                    modifier=defaultFieldModifier)

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



