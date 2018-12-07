
data <- read.csv("SolarPrediction.csv", header=TRUE)
nrow(data)
ncol(data)
head(data)
str(data)
colnames(data) <- c("unix_time","date","time", "radiation", "temperature", "pressure", "humidity", "wind_direction", "wind_speed", "sun_rise", "sun_set")

