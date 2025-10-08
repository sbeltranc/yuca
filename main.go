package main

import (
	"net/url"
	"os"
	"strings"

	"github.com/gofiber/fiber/v3"
	"github.com/gofiber/fiber/v3/middleware/cors"

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

	rootDomain := os.Getenv("APP_ROOT_DOMAIN")

	app.Use(cors.New(cors.Config{
		AllowCredentials: true,
		AllowHeaders:     []string{"Origin", "Content-Type", "Accept", "Authorization"},
		AllowOriginsFunc: func(origin string) bool {
			if rootDomain == "" {
				return false
			}

			parsedOrigin, err := url.Parse(origin)
			if err != nil {
				return false // malformed origin, bad :(
			}

			return parsedOrigin.Hostname() == rootDomain || strings.HasSuffix(parsedOrigin.Hostname(), "."+rootDomain)
		},
	}))

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
