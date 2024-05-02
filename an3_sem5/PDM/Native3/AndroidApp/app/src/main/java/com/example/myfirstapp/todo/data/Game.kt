package com.example.myfirstapp.todo.data
import androidx.room.Entity
import androidx.room.PrimaryKey
import com.example.myfirstapp.todo.ui.item.convertDateToString
import java.util.Date

@Entity
data class Game(
    @PrimaryKey val _id: String = "${System.currentTimeMillis()*10000}",
    val title: String = "",
    val lastVersion: String = "",
    val url: String = "",
    val totalReleases: Int = 0,
    val isOpenSource: Boolean = false,
    val platform: String="",
    val releaseDate: String = convertDateToString(Date()),
    val imageUrl: String="",
    var requiresCreate: Boolean=false,
    var requiresUpdate: Boolean=false
)