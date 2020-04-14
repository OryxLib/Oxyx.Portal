import { Injectable } from '@angular/core';

@Injectable()
export class socialMessage {
    fromId: string;
    toId: string;
    type: MessageType;
    content: string;
    isRead: boolean;
    timestamp: Number;
}

export enum MessageType {
    Text,
    Voice,
    Video,
    Image,
    Emoji,
    Gift,
    RedPacket,
    Money
}