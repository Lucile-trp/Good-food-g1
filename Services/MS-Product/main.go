package main

import (
	"github.com/gin-gonic/gin"
)

var db = make(map[string]string)

func setupRouter() *gin.Engine {
	r := gin.Default()
	return r
}

func main() {
	r := setupRouter()
	r.Run(":8080")
}
