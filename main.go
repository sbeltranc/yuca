package main

import (
	"os"

	"github.com/gofiber/fiber/v3"

	"github.com/goccy/go-json"
	"github.com/joho/godotenv"
)

var (
	version = "development"
	commit  = "none"
)

func main() {
	// loading the environment variables
	err := godotenv.Load()
	if err != nil {
		panic(err)
	}

	serverHeader := "yuca/" + version
	if commit != "none" {
		serverHeader += "-" + commit
	}

	// creating the application
	app := fiber.New(fiber.Config{
		AppName:      os.Getenv("APP_NAME"),
		ServerHeader: serverHeader,

		JSONEncoder: json.Marshal,
		JSONDecoder: json.Unmarshal,
	})

	app.Use(func(c fiber.Ctx) error {
		data := fiber.Map{
			"errors": []fiber.Map{
				{
					"code":    0,
					"message": "",
				},
			},
		}

		return c.Status(fiber.StatusNotFound).JSON(data)
	})

	// finally starting up the application
	app.Listen(":3000", fiber.ListenConfig{
		EnablePrefork:         true,
		DisableStartupMessage: false,
	})
}
