package com.example.myfirstapp.todo;

import android.Manifest
import android.app.NotificationChannel
import android.app.NotificationManager
import android.content.Context;
import android.content.pm.PackageManager
import android.os.Build
import android.os.Looper
import android.util.Log
import androidx.core.app.ActivityCompat
import androidx.core.app.NotificationCompat
import androidx.core.app.NotificationManagerCompat
import androidx.work.CoroutineWorker
import androidx.work.Worker

import androidx.work.WorkerParameters;
import com.example.myfirstapp.MyFirstApplication
import com.example.myfirstapp.R

@JvmField val VERBOSE_NOTIFICATION_CHANNEL_NAME: CharSequence = "Verbose WorkManager Notifications"
const val VERBOSE_NOTIFICATION_CHANNEL_DESCRIPTION = "Shows notifications whenever work starts"
@JvmField val NOTIFICATION_TITLE: CharSequence = "WorkRequest Starting"
const val CHANNEL_ID = "VERBOSE_NOTIFICATION"
const val NOTIFICATION_ID = 1

class PendingWorker(ctx:Context, params:WorkerParameters) : CoroutineWorker(ctx, params) {
    val TAG = "PendingWorker"

    override suspend fun doWork(): Result {
        val appContext = (applicationContext as MyFirstApplication)
        //makeStatusNotification("Pushing items to database", appContext)

        var successful:Int =0
        var failed:Int =0

        for(game in appContext.container.database.gameDao().getAll()){
            if(game.requiresUpdate) {
                Log.d("PendingWorker", "REQUIRES UPDATE ${game}")
                val res = appContext.container.itemRepository.update(game)
                if(res.requiresUpdate)
                    failed++
                else successful++
            }
            else if(game.requiresCreate) {
                Log.d("PendingWorker", "REQUIRES CREATE ${game}")
                appContext.container.itemRepository.quite_remove(game)
                val res = appContext.container.itemRepository.save(game)
                if (res.requiresCreate)
                    failed++
                else {
                    successful++
                }
            }
        }

        return try {
            if(successful!=0 || failed!=0)
                makeStatusNotification("Pushed items to database: ${successful} succeeded, ${failed} failed.", appContext)
            Result.success()
        } catch (throwable: Throwable) {
            Log.d(TAG, "PendingWorker error : "+throwable.message)
            Result.failure()
        }
    }
}


fun makeStatusNotification(message: String, context: Context) {

    // Make a channel if necessary
    // Create the NotificationChannel, but only on API 26+ because
    // the NotificationChannel class is new and not in the support library
    val name = VERBOSE_NOTIFICATION_CHANNEL_NAME
    val description = VERBOSE_NOTIFICATION_CHANNEL_DESCRIPTION
    val importance = NotificationManager.IMPORTANCE_HIGH
    val channel = NotificationChannel(CHANNEL_ID, name, importance)
    channel.description = description

    // Add the channel
    val notificationManager =
        context.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager?

    notificationManager?.createNotificationChannel(channel)

    // Create the notification
    val builder = NotificationCompat.Builder(context, CHANNEL_ID)
        .setSmallIcon(R.drawable.ic_launcher_foreground)
        .setContentTitle(NOTIFICATION_TITLE)
        .setContentText(message)
        .setPriority(NotificationCompat.PRIORITY_HIGH)
        .setVibrate(LongArray(0))

    // Show the notification
    if (ActivityCompat.checkSelfPermission(context, Manifest.permission.POST_NOTIFICATIONS) != PackageManager.PERMISSION_GRANTED)
        return
    NotificationManagerCompat.from(context).notify(NOTIFICATION_ID, builder.build())
}

