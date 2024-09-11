package controller

import (
	"net/http"
	"product/internal/entity"
	"product/internal/repo"
	"product/pkg/logger"

	"github.com/gin-gonic/gin"
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

	r := productRoutes{l, p}

	// Routers
	h := handler.Group("/products")
	{
		h.GET("/", r.GetProducts)
		h.POST("/", r.CreateProduct)
	}
}

func (r productRoutes) GetProducts(c *gin.Context) {
	products, err := r.p.GetProducts(c.Request.Context())
	if err != nil {
		r.l.Error(err, "GetProducts on  get products")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, products)
}

func (r productRoutes) CreateProduct(c *gin.Context) {
	var dish entity.Dish

	err := c.BindJSON(&dish)

	if err != nil {
		r.l.Error(err, "BindJSON on insert product")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Conversion errors"})

		return
	}

	err = r.p.InsertProduct(c.Request.Context(), dish)

	if err != nil {
		r.l.Error(err, "InsertProduct on insert product")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Database erros"})

		return
	}
}
