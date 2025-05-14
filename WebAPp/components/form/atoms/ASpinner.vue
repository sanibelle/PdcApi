<template>
  <div class="spinner-container" :style="containerStyle">
    <svg
      xmlns="http://www.w3.org/2000/svg"
      :viewBox="viewBox"
      :preserveAspectRatio="preserveAspectRatio"
      width="100%"
      height="100%"
    >
      <defs>
        <linearGradient id="gradient1" x1="0%" y1="0%" x2="100%" y2="100%">
          <stop offset="0%" :stop-color="primaryColor" />
          <stop offset="100%" :stop-color="secondaryColor" />
        </linearGradient>
        <linearGradient id="gradient2" x1="0%" y1="0%" x2="100%" y2="100%">
          <stop offset="0%" :stop-color="accentColor" />
          <stop offset="100%" :stop-color="secondaryColor" />
        </linearGradient>
        <filter id="glow" x="-20%" y="-20%" width="140%" height="140%">
          <feGaussianBlur stdDeviation="4" result="blur" />
          <feComposite in="SourceGraphic" in2="blur" operator="over" />
        </filter>
      </defs>

      <g :transform="transformPosition">
        <!-- Outer circle -->
        <circle class="circle outer-circle" cx="0" cy="0" :r="outerRadius" />

        <!-- Inner circle -->
        <circle class="circle inner-circle" cx="0" cy="0" :r="innerRadius" />

        <!-- Orbiting dot -->
        <g class="dot-orbit">
          <circle class="dot" cx="0" :cy="-orbitDistance" :r="dotRadius" />
        </g>

        <!-- Center dot -->
        <circle class="dot" cx="0" cy="0" :r="centerDotRadius" />
      </g>
    </svg>
  </div>
</template>

<script setup>
import { computed } from 'vue';

const props = defineProps({
  width: {
    type: [Number, String],
    default: 100,
  },
  height: {
    type: [Number, String],
    default: 15,
  },
  primaryColor: {
    type: String,
    default: '#6366f1',
  },
  secondaryColor: {
    type: String,
    default: '#8b5cf6',
  },
  accentColor: {
    type: String,
    default: '#ec4899',
  },
  speed: {
    type: Number,
    default: 3,
  },
  fitMode: {
    type: String,
    default: 'contain',
    validator: (value) => ['contain', 'cover', 'fill'].includes(value),
  },
});

const containerStyle = computed(() => {
  const width = typeof props.width === 'number' ? `${props.width}px` : props.width;
  const height = typeof props.height === 'number' ? `${props.height}px` : props.height;

  return {
    width: '100%',
    height: '100%',
    position: 'relative',
  };
});

const viewBox = computed(() => '0 0 200 200');

const preserveAspectRatio = computed(() => {
  if (props.fitMode === 'contain') {
    return 'xMidYMid meet';
  } else if (props.fitMode === 'cover') {
    return 'xMidYMid slice';
  } else {
    return 'none';
  }
});

const transformPosition = computed(() => 'translate(100, 100)');
const outerRadius = computed(() => 45);
const innerRadius = computed(() => 30);
const orbitDistance = computed(() => 60);
const dotRadius = computed(() => 4);
const centerDotRadius = computed(() => 6);
</script>

<style scoped>
.spinner-container {
  display: inline-block;
  overflow: visible;
}

.circle {
  fill: none;
  stroke-width: 4;
  stroke-linecap: round;
}

.outer-circle {
  stroke: url(#gradient1);
  stroke-dasharray: 283;
  transform-origin: center;
  filter: url(#glow);
  animation:
    rotate v-bind(speed + 's') cubic-bezier(0.8, 0.2, 0.2, 0.8) infinite,
    dash v-bind(speed + 's') cubic-bezier(0.4, 0, 0.6, 1) infinite;
}

.inner-circle {
  stroke: url(#gradient2);
  stroke-dasharray: 189;
  transform-origin: center;
  filter: url(#glow);
  animation:
    rotate-reverse v-bind(speed + 's') cubic-bezier(0.8, 0.2, 0.2, 0.8) infinite,
    dash-reverse v-bind(speed + 's') cubic-bezier(0.4, 0, 0.6, 1) infinite;
}

.dot {
  fill: white;
  transform-origin: center;
  filter: url(#glow);
  animation: pulse v-bind(speed + 's') cubic-bezier(0.4, 0, 0.6, 1) infinite;
}

.dot-orbit {
  transform-origin: center;
  animation: rotate v-bind(speed * 2 + 's') linear infinite;
}

@keyframes rotate {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}

@keyframes rotate-reverse {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(-360deg);
  }
}

@keyframes dash {
  0% {
    stroke-dashoffset: 0;
  }
  50% {
    stroke-dashoffset: 283;
  }
  100% {
    stroke-dashoffset: 0;
  }
}

@keyframes dash-reverse {
  0% {
    stroke-dashoffset: 0;
  }
  50% {
    stroke-dashoffset: -189;
  }
  100% {
    stroke-dashoffset: 0;
  }
}

@keyframes pulse {
  0%,
  100% {
    transform: scale(0.8);
    opacity: 0.7;
  }
  50% {
    transform: scale(1.2);
    opacity: 1;
  }
}
</style>
