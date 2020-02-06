import pygame, sys, math
from pygame.locals import *

def terminate():
	pygame.quit()
	sys.exit()

def saveMap(board):
	lineCount = 0
	with open ('Maps.txt', 'w') as mapFile:
		for line in board:
			stringLine = ''
			for item in line:
				stringLine += str(item) + ' '
			
			mapFile.write(stringLine.strip(' ') + '\n')
			lineCount += 1

def transform(coordinates):
	# Transforms passed pair of global window coordinates into board coordinates
	return [math.floor(coordinates[0] / CELL), math.floor(coordinates[1] / CELL)]

# Set up main constants
FPS = 30
CELL = 20
BOARD_SIZE = 22
WINDOW_SIZE = BOARD_SIZE * CELL

WALL = (0, 0, 128)
KILLER = (255, 0, 0)
FIELD = (255,255,0)
PIT = (0, 128, 0)

board = []
for i in range(BOARD_SIZE):
	board.append([])
	for j in range(BOARD_SIZE):
		board[i].append(0)

# Read map
lineCount = 0
with open ('Maps.txt', 'r') as mapFile:
	for line in mapFile:
		line = line.split(" ")
		counter = 0
		for item in line:
			line[counter] = int(item)
			counter += 1
		board[lineCount] = line
		lineCount += 1

pygame.init()
windowSurface = pygame.display.set_mode((WINDOW_SIZE, WINDOW_SIZE))
pygame.display.set_caption('Map Editor (press S to save map)')
mainClock = pygame.time.Clock()

while True:
	for event in pygame.event.get():
		if event.type == QUIT:
			terminate()
		if event.type == KEYDOWN:
			if event.key == K_ESCAPE:
				terminate()
			if event.key == K_s:
				saveMap(board)
		if pygame.mouse.get_pressed()[0]:
			if event.type == MOUSEBUTTONDOWN:
				x, y = transform(pygame.mouse.get_pos())
				if board[y][x] < 3: 
					board[y][x] += 1
				else:
					board[y][x] = 0

	for y in range(BOARD_SIZE):
		for x in range(BOARD_SIZE):
			if board[y][x] == 0:
				color = FIELD
			elif board[y][x] == 1:
				color = WALL
			elif board[y][x] == 2:
				color = KILLER
			else:
				color = PIT
			pygame.draw.rect(windowSurface, color, pygame.Rect(x * CELL, y * CELL, CELL, CELL))

	pygame.display.update()
	mainClock.tick(FPS)





