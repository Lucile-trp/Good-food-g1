package controller

import (
	"net/http"
	"produit/internal/entity"
	"produit/internal/repo"
	"produit/pkg/logger"

	"github.com/gin-gonic/gin"

	swaggerFiles "github.com/swaggo/files"
	ginSwagger "github.com/swaggo/gin-swagger"
)

type productRoutes struct {
	l logger.Interface
	p repo.ProductRepo
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
		c.AbortWithStatusJSON(http.StatusInternalServerError, entity.Error{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, products)
}
