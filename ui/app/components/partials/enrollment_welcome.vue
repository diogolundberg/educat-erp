<template>
  <SlideTransition class="p4 center">
    <div
      v-if="page == 1"
      :key="1">
      <img src="../../assets/img/logo.png">
      <h2 class="m4 dim">Bem vindo!</h2>
      <Btn
        label="Iniciar Matrícula"
        big
        primary
        @click="page = 2" />
    </div>
    <div
      v-else
      :key="2">
      <div
        slot="title"
        class="center mb4n mt2">
        <UploadButton
          v-model="enrollment.photo"
          class="m3"
          prefix="picture"
          @input="savePhoto">
          <img
            v-if="enrollment.photo"
            :src="enrollment.photo"
            class="rounded shadow2 width-1 height-1 bg-white">
          <span
            v-if="!enrollment.photo"
            class="relative">
            <img
              src="../../assets/img/people.svg"
              class="rounded shadow2 width-1 height-1 bg-white">
            <div
              class="absolute bottom-0 right-0 left-0 m-auto m1 p1 bg-white shadow1">
              Clique para inserir sua foto
            </div>
            <small class="block">
              Não se preocupe, você pode trocar depois
            </small>
          </span>
        </UploadButton>
        <h2>Como você quer ser chamado?</h2>
        <div class="my2">
          <InputBox
            v-model="enrollment.personalData.assumedName"
            label="Seu nome social"
            class="width-2 m-auto" />
        </div>
        <Btn
          label="Pronto!"
          big
          primary
          @click="$emit('done')" />
      </div>
    </div>
  </SlideTransition>
</template>

<script>
  export default {
    name: "EnrollmentWelcome",
    props: {
      enrollment: {
        type: Object,
        default: () => ({}),
      },
    },
    data() {
      return {
        page: 1,
      };
    },
  };
</script>
