package com.example.myfirstapp.todo.ui.item

import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.KeyboardType

@Composable
fun MyNumberField(value:Int, onValueChanged:(Int)->Unit, modifier:Modifier=Modifier, label:String="") {
    var text by remember { mutableStateOf(value.toString())}

    val change : (String) -> Unit = { it ->
        text = it
        onValueChanged(text.toIntOrNull()?:0)
    }

    TextField(
        value = text,
        modifier = modifier.fillMaxWidth(),
        keyboardOptions = KeyboardOptions.Default.copy(keyboardType = KeyboardType.Number),
        onValueChange = change,
        label = {Text(label)},
    )

}