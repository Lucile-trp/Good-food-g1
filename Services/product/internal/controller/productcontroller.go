package controller

import (
	"net/http"
	"product/internal/repo"
	"product/pkg/logger"

	"github.com/gin-gonic/gin"

	swaggerFiles "github.com/swaggo/files"
	ginSwagger "github.com/swaggo/gin-swagger"
)

type productRoutes struct {
	l logger.Interface
	p repo.ProductRepo
}

type errorMessage struct {
	Message string `json:"error"`
}

func NewRouter(handler *gin.Engine, l logger.Interface, p repo.ProductRepo) {
	handler.Use(gin.Logger())
	handler.Use(gin.Recovery())

	swaggerHandler := ginSwagger.DisablingWrapHandler(swaggerFiles.Handler, "DISABLE_SWAGGER_HTTP_HANDLER")
	handler.GET("/swagger/*any", swaggerHandler)

	r := productRoutes{l, p}

	// Routers
	h := handler.Group("/products")
	{
		h.GET("/", r.GetProducts)
	}
}

func (r productRoutes) GetProducts(c *gin.Context) {
	products, err := r.p.GetProducts(c.Request.Context())
	if err != nil {
		r.l.Error(err, "GetProducts on product")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, products)
}
