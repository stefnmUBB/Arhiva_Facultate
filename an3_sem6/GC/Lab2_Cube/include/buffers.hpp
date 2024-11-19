#pragma once

static constexpr float vertices[] = {
	 1.0f,  1.0f, -1.0f,
	-1.0f, -1.0f, -1.0f,
	-1.0f, -1.0f,  1.0f,
	 1.0f, -1.0f, -1.0f,
	-1.0f,  1.0f,  1.0f,
	 1.0f,  1.0f,  1.0f,
	 1.0f, -1.0f,  1.0f,
	-1.0f,  1.0f, -1.0f
};

static constexpr unsigned int triangles[] = {
	7, 5, 0,
	5, 2, 6,
	4, 1, 2,
	3, 2, 1,
	0, 6, 3,
	7, 3, 1,
	7, 4, 5,
	5, 4, 2,
	4, 7, 1,
	3, 6, 2,
	0, 5, 6,
	7, 0, 3
};