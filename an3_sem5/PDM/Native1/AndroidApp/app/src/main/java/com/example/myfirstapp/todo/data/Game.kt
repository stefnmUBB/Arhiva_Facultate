package com.example.myfirstapp.todo.data
import com.example.myfirstapp.todo.ui.item.convertDateToString
import java.util.Date
data class Game(val _id: String = "",
                val title: String = "",
                val lastVersion: String = "",
                val url: String = "",
                val totalReleases: Int = 0,
                val isOpenSource: Boolean = false,
                val platform: String="",
                val releaseDate: String = convertDateToString(Date()))