package com.example.myfirstapp.todo.ui.items

import android.content.Context
import android.hardware.Sensor
import android.hardware.SensorEvent
import android.hardware.SensorEventListener
import android.hardware.SensorManager
import android.util.Log
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.animation.ExperimentalAnimationApi
import androidx.compose.animation.core.LinearEasing
import androidx.compose.animation.core.animateFloat
import androidx.compose.animation.core.infiniteRepeatable
import androidx.compose.animation.core.keyframes
import androidx.compose.animation.core.rememberInfiniteTransition
import androidx.compose.animation.core.tween
import androidx.compose.animation.expandVertically
import androidx.compose.animation.fadeIn
import androidx.compose.animation.scaleIn
import androidx.compose.animation.slideInVertically
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.DisposableEffect
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.geometry.Offset
import androidx.compose.ui.graphics.Brush
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.TileMode
import androidx.compose.ui.graphics.graphicsLayer
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.platform.LocalDensity
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.myfirstapp.todo.data.Game
import com.example.myfirstapp.ui.theme.LocalCustomColorsPalette
import kotlinx.coroutines.delay

typealias OnItemFn = (id: String?) -> Unit

@Composable
fun ItemList(gameList: List<Game>, onItemClick: OnItemFn, modifier: Modifier) {
    val sensorManager = LocalContext.current.getSystemService(Context.SENSOR_SERVICE) as SensorManager
    val gyroscopeSensor = sensorManager.getDefaultSensor(Sensor.TYPE_GYROSCOPE)

    var rotX by remember{ mutableStateOf(0f)}
    var rotY by remember{ mutableStateOf(0f)}
    var rotZ by remember{ mutableStateOf(0f)}

    DisposableEffect(sensorManager, gyroscopeSensor) {
        val gyroscopeListener = object : SensorEventListener {
            override fun onAccuracyChanged(sensor: Sensor?, accuracy: Int) {}

            override fun onSensorChanged(event: SensorEvent) {
                rotX = event.values[0]
                rotY = event.values[1]
                rotZ = event.values[2]
            }
        }

        sensorManager.registerListener(
            gyroscopeListener,
            gyroscopeSensor,
            SensorManager.SENSOR_DELAY_NORMAL
        )

        onDispose {
            sensorManager.unregisterListener(gyroscopeListener)
        }
    }

    Log.d("ItemList", "recompose")
    LazyColumn(
        modifier = modifier
            .fillMaxSize()
            .padding(12.dp)
            .graphicsLayer(
                rotationX = -rotX * 180f / 3.14f / 10f,
                rotationY = -rotY * 180f / 3.14f / 10f,
                rotationZ = -rotZ * 180f / 3.14f / 10f,
            )
    ) {
        items(gameList) { item ->
            ItemDetail(item, onItemClick)
        }
    }
}

@OptIn(ExperimentalAnimationApi::class)
@Composable
fun ItemDetail(game: Game, onItemClick: OnItemFn) {
    val sensorManager = LocalContext.current.getSystemService(Context.SENSOR_SERVICE) as SensorManager
    val accelerometerSensor = sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER)

    var visible by remember { mutableStateOf(false) }
    val density = LocalDensity.current
    //Log.d("ItemDetail", "recompose id = ${game._id}, title: ${game.title}")
    var accX by remember{ mutableStateOf(0f)}
    var accY by remember{ mutableStateOf(0f)}
    var accZ by remember{ mutableStateOf(0f)}

    var buttonLoc by remember{ mutableStateOf(-1)}

    DisposableEffect(sensorManager, accelerometerSensor) {
        val accelerometerListener = object : SensorEventListener {
            override fun onAccuracyChanged(sensor: Sensor?, accuracy: Int) {}

            override fun onSensorChanged(event: SensorEvent) {
                accX = event.values[0]
                accY = event.values[1]
                accZ = event.values[2]
                if(accX>20) {
                    println("LEFT")
                    buttonLoc = -1
                } else if(accX<-20) {
                    println("RIGHT")
                    buttonLoc=1
                }
            }
        }

        sensorManager.registerListener(accelerometerListener, accelerometerSensor, SensorManager.SENSOR_DELAY_NORMAL)

        onDispose {
            sensorManager.unregisterListener(accelerometerListener)
        }
    }

    val currentFontSizePx = with(LocalDensity.current) { 24.dp.toPx() }
    val currentFontSizeDoublePx = currentFontSizePx * 2

    val infiniteTransition = rememberInfiniteTransition()
    val offset by infiniteTransition.animateFloat(
        initialValue = 0f,
        targetValue = currentFontSizeDoublePx,
        animationSpec = infiniteRepeatable(tween(1000, easing = LinearEasing))
    )
    val brush = Brush.linearGradient(
        listOf(Color.Magenta, Color.Cyan, Color.White),
        start = Offset(offset, offset),
        end = Offset(offset + currentFontSizePx, offset + currentFontSizePx),
        tileMode = TileMode.Mirror
    )

    LaunchedEffect(true){
        delay(200)
        visible=true
    }

    AnimatedVisibility(
        visible = visible,
        enter = slideInVertically {
            with(density) { -40.dp.roundToPx() }
        } + scaleIn(
            initialScale = 0.0f,
            animationSpec = keyframes { this.durationMillis = 700 }
        ) + fadeIn(
            initialAlpha = 0.5f,
            animationSpec = keyframes { this.durationMillis = 700 }
        ),
        modifier = Modifier.fillMaxWidth()
    ) {
        Row(modifier = Modifier.fillMaxWidth()) {

            Column(
                modifier = Modifier
                    .padding(16.dp)
                    .fillMaxWidth()
                    .clickable { onItemClick(game._id) }
            ) {
                Text(
                    text = game.title,
                    style = TextStyle(
                        fontSize = 24.sp,
                        fontWeight = FontWeight.Bold,
                        brush=if (!(game.requiresCreate || game.requiresUpdate)) brush else null,
                    ),
                    modifier = Modifier.align(if(buttonLoc<0) Alignment.Start else Alignment.End),
                    //modifier = Modifier.align(Alignment.End),
                    color = if (game.requiresCreate || game.requiresUpdate)
                        LocalCustomColorsPalette.current.pendingOp else Color.Unspecified
                )
                Text(
                    text = if (game.requiresCreate) "(to be created)" else if (game.requiresUpdate) "(to be updated)" else "",
                    modifier = Modifier.align(if(buttonLoc<0) Alignment.Start else Alignment.End),
                    style = TextStyle(
                        fontSize = 18.sp,
                        fontStyle = FontStyle.Italic
                    )
                )
                Text(
                    text = "V${game.lastVersion}",
                    modifier = Modifier.align(if(buttonLoc<0) Alignment.Start else Alignment.End),
                    style = TextStyle(
                        fontSize = 16.sp,
                        color = Color.Gray
                    )
                )
            }
        }
    }
}
