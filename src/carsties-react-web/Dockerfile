# Use an official Node.js runtime as a parent image
FROM node:latest

# Set working directory
WORKDIR /app

# Copy package.json and package-lock.json (or yarn.lock) into the working directory
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the Next.js app
RUN npm run build

# Expose the desired port (default for Next.js is 3000)
EXPOSE 3000

# Start the app
CMD ["npm", "start"]