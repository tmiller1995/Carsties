# Stage 1: Build the application
FROM oven/bun:latest AS builder
ARG FONTAWESOME_PACKAGE_TOKEN

WORKDIR /app

RUN echo "@fortawesome:registry=https://npm.fontawesome.com/" > .npmrc && \
    echo "@awesome.me:registry=https://npm.fontawesome.com/" >> .npmrc && \
    echo "//npm.fontawesome.com/:_authToken=${FONTAWESOME_PACKAGE_TOKEN}" >> .npmrc

# Copy package.json and configuration files
COPY package.json ./
COPY bun.lock* ./
COPY tsconfig*.json ./

# Install dependencies without the frozen lockfile flag
RUN bun install

# Remove the .npmrc file we created
RUN rm .npmrc
# Copy the rest of the application
COPY . .

# Build the application
RUN bun run build

# Stage 2: Production image
FROM oven/bun:latest

WORKDIR /app

# Set environment variables
ENV NODE_ENV=production
ENV PORT=3000

# Copy only necessary files from builder
COPY --from=builder /app/public ./public
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/package.json ./package.json
COPY --from=builder /app/node_modules ./node_modules

# Expose the port the app will run on
EXPOSE 3000

# Start the application
CMD ["bun", "--bun", "next", "start"]