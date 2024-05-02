package com.example.myfirstapp.todo

import android.content.Context
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.graphics.ImageDecoder
import android.util.Log
import androidx.activity.compose.rememberLauncherForActivityResult
import androidx.activity.result.contract.ActivityResultContracts
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.unit.dp
import androidx.core.content.FileProvider
import coil.compose.AsyncImage
import com.example.myfirstapp.BuildConfig
import java.io.ByteArrayOutputStream
import java.io.File
import java.text.SimpleDateFormat
import java.util.Base64
import java.util.Date
import java.util.Objects

//@OptIn(ExperimentalPermissionsApi::class)
@Composable
fun ImagePicker(originalUri:String, uriChanged:(String)->Unit) {
    val context = LocalContext.current
    var currentPhotoUrl by remember { mutableStateOf(value = originalUri) }

    val file = context.createImageFile()
    val uri = FileProvider.getUriForFile(
        Objects.requireNonNull(context),
        BuildConfig.APPLICATION_ID+ ".provider", file)

    val cameraLauncher = rememberLauncherForActivityResult(
        contract = ActivityResultContracts.TakePicture(),
        onResult = { success ->
            if (success) {
                val bmp = ImageDecoder.decodeBitmap(ImageDecoder.createSource(context.contentResolver, uri))
                val scaled = Bitmap.createScaledBitmap(bmp, 128, 128, false)

                val byteArrayOutputStream = ByteArrayOutputStream()
                scaled.compress(Bitmap.CompressFormat.JPEG, 100, byteArrayOutputStream)
                val byteArray: ByteArray = byteArrayOutputStream.toByteArray()
                val encoded:String = "data:image/jpg;base64,"+ Base64.getEncoder().encodeToString(byteArray);

                currentPhotoUrl = encoded
                //Log.d("ESTEEEEEEEEEEEEE", currentPhotoUrl)
                uriChanged(currentPhotoUrl)
            }
        }
    )

    /*val cameraPermissionState = rememberPermissionState(
        permission = Manifest.permission.CAMERA,
        onPermissionResult = { granted ->
            if (granted) {
                cameraLauncher.launch(uri)
            } else print("camera permission is denied")
        }
    )*/

    fun toBitmap(url64:String):Bitmap {
        val data64 = url64.substring("data:image/jpg;base64,".length)
        val bytes = Base64.getDecoder().decode(data64)
        val bmp = BitmapFactory.decodeByteArray(bytes, 0, bytes.size)
        return bmp.copy(Bitmap.Config.ARGB_8888, true)
    }

    Column(
        modifier = Modifier.fillMaxSize(),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        AnimatedVisibility(visible = currentPhotoUrl.isNotEmpty()) {
            // from coil library
            AsyncImage(
                modifier = Modifier.size(size = 240.dp),
                model = toBitmap(currentPhotoUrl),
                contentDescription = null
            )
        }
        Button(onClick = {cameraLauncher.launch(uri)}) {
            Text(text = "Take a photo with Camera")
        }
    }
}

fun Context.createImageFile(): File {
    // Create an image file name
    val timeStamp = SimpleDateFormat("yyyyMMdd_HHmmss").format(Date())
    val imageFileName = "JPEG_" + timeStamp + "_"
    return File.createTempFile(
        imageFileName, /* prefix */
        ".jpg", /* suffix */
        externalCacheDir /* directory */
    )
}