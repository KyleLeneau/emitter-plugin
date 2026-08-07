import { Controller, Post, Req } from '@nestjs/common';
import type { FastifyRequest } from 'fastify';

@Controller('webhook')
export class WebhookController {
  @Post()
  handle(@Req() req: FastifyRequest) {
    console.log('Headers:', req.headers);
    console.log('Body:', req.body);
    return { status: 'received' };
  }
}
