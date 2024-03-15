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

func NewRouter(handler *gin.Engine, l logger.Interface, r repo.TranslationRepo) {
	handler.Use(gin.Logger())
	handler.Use(gin.Recovery())

	swaggerHandler := ginSwagger.DisablingWrapHandler(swaggerFiles.Handler, "DISABLE_SWAGGER_HTTP_HANDLER")
	handler.GET("/swagger/*any", swaggerHandler)

	// Routers
	h := handler.Group("/products")
	{
		h.GET("/")
	}
}

func GetProducts(c *gin.Context, l logger.Interface, r *repo.TranslationRepo) {
	products, err := r.GetProducts(c.Request.Context())
	if err != nil {
		l.Error(err, "GetProducts on product")
		c.AbortWithStatusJSON(http.StatusInternalServerError, entity.Error{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, products)
}
