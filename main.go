package main

import (
	"github.com/gofiber/fiber/v3"
	"github.com/joho/godotenv"
)

func main() {
	// loading the environment variables
	err := godotenv.Load()
	if err != nil {
		panic(err)
	}

	// creating the application
	app := fiber.New()

	app.Get("/", func(c fiber.Ctx) error {
		return c.SendString("Hello, World!")
	})

	app.Listen(":3000")
}
