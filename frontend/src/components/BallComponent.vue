<script lang="ts" setup>
import { ref } from 'vue';

const isActive = ref<boolean>(false);
const isShaking = ref<boolean>(false);
const currentAnswer = ref<string>('');

// const answers: string[] = [
//   // Positive answers
//   'It is certain',
//   'It is decidedly so',
//   'Without a doubt',
//   'Yes definitely',
//   'You may rely on it',
//   'As I see it, yes',
//   'Most likely',
//   'Outlook good',
//   'Yes',
//   'Signs point to yes',

//   // Non-committal answers
//   'Reply hazy, try again',
//   'Ask again later',
//   'Better not tell you now',
//   'Cannot predict now',
//   'Concentrate and ask again',

//   // Negative answers
//   "Don't count on it",
//   'My reply is no',
//   'My sources say no',
//   'Outlook not so good',
//   'Very doubtful',
// ];

const getRandomAnswer = async (): Promise<string> => {
  try {
    const response = await fetch('http://localhost:3000/answer');
    const data = await response.json();
    return data.answer;
  } catch (error) {
    console.error('Error fetching answer:', error);
    return 'Ask again later'; // fallback answer
  }
};
const handleBallClick = async (): Promise<void> => {
  if (isActive.value) return;
  isShaking.value = true;
  setTimeout(async () => {
    isShaking.value = false;
    isActive.value = true;
    currentAnswer.value = await getRandomAnswer();

    setTimeout(() => {
      isActive.value = false;
    }, 4000);
  }, 1000);
};
</script>

<template>
  <div class="ball-container">
    <h1>Magic 8-Ball</h1>
    <p>Click and Shake the ball to get your answer</p>
    <div class="image-wrapper" :class="{ shake: isShaking }">
      <img
        v-show="!isActive"
        @click="handleBallClick"
        src="../images/ball.png"
        alt="magic 8-ball image"
      />
      <img
        class="empty-img"
        v-show="isActive"
        src="../images/empty.png"
        alt="magic 8-ball image"
      />
      <div v-show="isActive" class="empty">
        <div class="triangle">
          <span>{{ currentAnswer }}</span>
        </div>
      </div>
    </div>
  </div>
</template>

<style>
.ball-container {
  text-align: center;
}
p {
  margin: 0;
  font-size: 2em;
  color: rgb(3, 32, 34);
}
.image-wrapper {
  position: relative;
  display: inline-block;
}

.image-wrapper.shake {
  animation-name: shake;
  animation-duration: 0.3s;
  animation-iteration-count: 11;
  animation-timing-function: ease-in-out;
}

h1 {
  text-align: center;
  font-family: 'Arial', sans-serif;
  color: #000000;
}

img {
  cursor: pointer;
  border-radius: 50%;
  height: auto;
  display: block;
}

.empty {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  height: 400px;
  width: 400px;
  background: rgba(255, 255, 255, 0.9);
  border-radius: 50%;
  border: 2px solid #333;
  display: flex;
  align-items: center;
  justify-content: center;
}

.triangle {
  width: 0;
  height: 0;
  border-left: 140px solid transparent;
  border-right: 140px solid transparent;
  border-bottom: 300px solid #00009c;

  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  color: white;
  font-weight: bold;
  font-size: 26px;
  transform: rotate(180deg);
  margin-top: 30px;
}
span {
  /* display: block; */
  padding-bottom: 380px;
  /* width: 280px; */
  transform: rotate(180deg);
}

@keyframes shake {
  0% {
    transform: translateX(0);
  }
  25% {
    transform: translateX(-5px);
  } /* Move left */
  50% {
    transform: translateX(5px);
  } /* Move right */
  75% {
    transform: translateX(-5px);
  } /* Move left again */
  100% {
    transform: translateX(0);
  } /* Return to original position */
}
</style>
