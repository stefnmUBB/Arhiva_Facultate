package com.example.myfirstapp.todo.ui.item

import android.annotation.SuppressLint
import androidx.compose.foundation.clickable
import androidx.compose.foundation.interaction.MutableInteractionSource
import androidx.compose.foundation.interaction.PressInteraction
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.material3.Button
import androidx.compose.material3.DatePicker
import androidx.compose.material3.DatePickerDialog
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.SelectableDates
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.rememberDatePickerState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.layout.onGloballyPositioned
import androidx.compose.ui.unit.toSize
import java.text.SimpleDateFormat
import java.util.Date


@SuppressLint("SimpleDateFormat")
fun convertDateToString(date:Date):String {
    val formatter = SimpleDateFormat("dd/MM/yyyy")
    return formatter.format(date)
}

@SuppressLint("SimpleDateFormat")
fun convertStringToDate(str:String):Date{
    val formatter = SimpleDateFormat("dd/MM/yyyy")
    return formatter.parse(str) ?: Date()
}
private fun convertMillisToDate(millis: Long): String { return convertDateToString(Date(millis)) }


@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun MyDatePickerDialog(
    onDateSelected: (String) -> Unit,
    onDismiss: () -> Unit
) {
    val datePickerState = rememberDatePickerState(selectableDates = object : SelectableDates {
        override fun isSelectableDate(utcTimeMillis: Long): Boolean {
            return utcTimeMillis <= System.currentTimeMillis()
        }
    })

    val selectedDate = datePickerState.selectedDateMillis?.let {
        convertMillisToDate(it)
    } ?: ""

    DatePickerDialog(
        onDismissRequest = { onDismiss() },
        confirmButton = {
            Button(onClick = {
                onDateSelected(selectedDate)
                onDismiss()
            }){
                Text(text="Ok", onTextLayout={})
            }
        },
        dismissButton = {
            Button(onClick = {
                onDismiss()
            }) {
                Text(text = "Cancel", onTextLayout={})
            }
        }
    ) {
        DatePicker(
            state = datePickerState
        )
    }
}
@Composable
fun MyDatePickerDialog(date:String, onDateSelected:(String)->Unit, modifier: Modifier = Modifier, label:String="") {
    var _date by remember {  mutableStateOf(date) }

    var showDatePicker by remember { mutableStateOf(false)  }

    Box(contentAlignment = Alignment.Center) {
        TextField(
            value = _date,
            onValueChange = { },
            modifier = modifier.fillMaxWidth(),
            interactionSource = remember { MutableInteractionSource() }
                .also { interactionSource ->
                    LaunchedEffect(interactionSource) {
                        interactionSource.interactions.collect {
                            if (it is PressInteraction.Release) {
                                showDatePicker = true
                            }
                        }
                    }
                },
            label = { Text(label) },
            //readOnly = true
        )
    }

    if (showDatePicker) {
        MyDatePickerDialog(
            onDateSelected = { _date = it; onDateSelected(_date) },
            onDismiss = { showDatePicker = false }
        )
    }
}