#pragma once

typedef struct
{
	const char* options[100];
	int opt_count;
} ui;

/// <summary>
/// creates user interface
/// </summary>
/// <returns>pointer to UI instance</returns>
ui* create_ui();

/// <summary>
/// Adds display text option to ui
/// </summary>
/// <param name="ui">user interface</param>
/// <param name="text">display text</param>
void add_ui_option(ui* ui, const char* text);

/// <summary>
/// Gets command id from user input
/// </summary>
/// <param name="ui">user interface</param>
/// <returns>command code</returns>
int request_ui_option(ui* ui);
