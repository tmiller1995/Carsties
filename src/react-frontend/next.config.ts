import type { NextConfig } from "next";

const nextConfig: NextConfig = {
    logging: {
        fetches: {
            fullUrl: true
        }
    },
    images: {
        remotePatterns: [
            {
                protocol: 'https',
                hostname: 'via.placeholder.com',
                pathname: '/**'
            }
        ]
    }
};

export default nextConfig;
