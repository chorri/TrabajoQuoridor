import sys, pygame
#Iniciamos pygame
pygame.init()
#Muestro una ventana de 800 x 600
size = 800, 600
screen= pygame.display.set_mode(size)
#Cambio el titulo en ventana
pygame.display.set_caption("QUORIDOR")
#Inicia el bucle
run = True
while run:
    for event in pygame.event.get():
        if event.type == pygame.QUIT: run = False
pygame.quit()




